// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Daml.Ledger.Api.Data.Util.Test
{
    public class SingleTest
    {
        [Fact]
        public void CanCreateSingleForImmediateValue()
        {
            var single = Single.Just(5);

            single.Result.Should().Be(5);

            int notificationCount = 0;
            int errorCount = 0;
            int values = 0;

            using (var subscription = single.Subscribe(v => { ++notificationCount; values += v; }, e => ++errorCount))
            { }

            using (var subscription = single.Subscribe(v => { ++notificationCount; values += v; }, e => ++errorCount))
            { }

            notificationCount.Should().Be(2);
            errorCount.Should().Be(0);
            values.Should().Be(10);
        }

        [Fact]
        public void CanCreateFailedSingle()
        {
            var exception = new Exception("failed");

            var single = Single.Error<int>(exception);

            int notificationCount = 0;

            var errors = new List<Exception>();

            using (var subscription = single.Subscribe(v => ++notificationCount, e => errors.Add(e)))
            { }

            using (var subscription = single.Subscribe(v => ++notificationCount, e => errors.Add(e)))
            { }

            notificationCount.Should().Be(0);
            errors.Count.Should().Be(2);
            errors[0].Should().Be(exception);
            errors[1].Should().Be(exception);
        }

        [Fact]
        public void AccessingResultForFailedSingleResultsInException()
        {
            var single = Single.Error<int>(new Exception("failed"));

            try
            {
                int val = single.Result;

            }
            catch (Exception e)
            {
                e.Message.Should().Be("One or more errors occurred. (failed)");
            }
        }
        
        [Fact]
        public void CanCreateSingleFromCompletedTask()
        {
            var single = Single.Just(Task.FromResult(5));

            single.Result.Should().Be(5);
        }

        [Fact]
        public void WillBlockOnResultOfAsyncTask()
        {
            int millisDelay = 2000;

            Task<int> task = new Task<int>(() => { Thread.Sleep(millisDelay); return 5; });

            var single = Single.Just(task);

            Stopwatch timer = new Stopwatch();

            timer.Start();
            task.Start();

            int val = single.Result;

            timer.Stop();

            val.Should().Be(5);

            timer.ElapsedMilliseconds.Should().BeInRange(millisDelay - 20, millisDelay + 20);
        }

        [Fact]
        public void IsNotifiedByAsyncTask()
        {
            int millisDelay = 2000;

            Task<int> task = new Task<int>(() => { Thread.Sleep(millisDelay); return 5; });

            var single = Single.Just(task);

            DateTime notificationTime = DateTime.MinValue;
            Exception notifiedException = null;

            using (var subscription = single.Subscribe(v =>  notificationTime = DateTime.UtcNow, e => { notifiedException = e; }))
            {
                DateTime startTime = DateTime.UtcNow;

                task.Start();

                // Block until we are notified which might be after the single is completed because of 'asynchronicity'!
                while (notificationTime == DateTime.MinValue)
                {
                    Thread.Sleep(500);
                    if ((DateTime.UtcNow - startTime).TotalSeconds > 10)    // Just in case
                        break;
                }

                var notificationDelay = (notificationTime - startTime).TotalMilliseconds;

                notifiedException.Should().BeNull();
                notificationDelay.Should().BeInRange(millisDelay - 20, millisDelay + 20);
            }
        }

        [Fact]
        public void CancellingAsyncTaskThrowsErrorToObserver()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            Task<int> task = new Task<int>(() =>
            {
                Thread.Sleep(20000);
                return 5;
            }, cancellationTokenSource.Token);

            var single = Single.Just(task);

            DateTime notificationTime = DateTime.MinValue;
            Exception notifiedException = null;

            using (var subscription = single.Subscribe(v => notificationTime = DateTime.UtcNow, e => { notifiedException = e; }))
            {
                DateTime startTime = DateTime.UtcNow;

                task.Start();

                cancellationTokenSource.Cancel();

                // Block until we are notified which might be after the single is completed because of 'asynchronicity'!
                while (notificationTime == DateTime.MinValue && notifiedException == null)
                {
                    Thread.Sleep(500);
                    if ((DateTime.UtcNow - startTime).TotalSeconds > 10)    // Just in case
                        break;
                }

                notificationTime.Should().Be(DateTime.MinValue, "have been notified after cancellation");
                notifiedException.Should().NotBeNull("exception not notified");
                notifiedException.Message.Should().Be("A task was canceled.");
            }
        }
    }
}
