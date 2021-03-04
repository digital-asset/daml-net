// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Daml.Ledger.Api.Data.Util.Test
{
    [TestFixture]
    public class SingleTest
    {
        [Test]
        public void CanCreateSingleForImmediateValue()
        {
            var single = Single.Just(5);

            Assert.AreEqual(5, single.Result);

            int notificationCount = 0;
            int errorCount = 0;
            int values = 0;

            using (var subscription = single.Subscribe(v => { ++notificationCount; values += v; }, e => ++errorCount))
            { }

            using (var subscription = single.Subscribe(v => { ++notificationCount; values += v; }, e => ++errorCount))
            { }

            Assert.AreEqual(2, notificationCount);
            Assert.AreEqual(0, errorCount);
            Assert.AreEqual(10, values);
        }

        [Test]
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

            Assert.AreEqual(0, notificationCount);
            Assert.AreEqual(2, errors.Count);
            Assert.AreEqual(exception, errors[0]);
            Assert.AreEqual(exception, errors[1]);
        }

        [Test]
        public void AccessingResultForFailedSingleResultsInException()
        {
            var single = Single.Error<int>(new Exception("failed"));

            try
            {
                int val = single.Result;

            }
            catch (Exception e)
            {
                Assert.AreEqual("One or more errors occurred. (failed)", e.Message);
            }
        }
        
        [Test]
        public void CanCreateSingleFromCompletedTask()
        {
            var single = Single.Just(Task.FromResult(5));

            Assert.AreEqual(5, single.Result);
        }

        [Test]
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

            Assert.AreEqual(5, val);

            Assert.AreEqual(millisDelay, timer.ElapsedMilliseconds, 20, "Blocking delay different then expected");
        }

        [Test]
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

                Assert.IsNull(notifiedException);
                Assert.AreEqual(millisDelay, notificationDelay, 20, $"Notification delay different then expected : got {notificationDelay} : expected {millisDelay}");
            }
        }

        [Test]
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

                Assert.AreEqual(DateTime.MinValue, notificationTime, "Have been notified after cancellation");
                Assert.IsNotNull(notifiedException, "Exception not notified");
                Assert.AreEqual("A task was canceled.", notifiedException.Message);
            }
        }

    }
}
