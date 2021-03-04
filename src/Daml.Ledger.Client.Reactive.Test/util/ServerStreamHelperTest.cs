// Copyright(c) 2021 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace Daml.Ledger.Client.Reactive.Test.Util
{
    using Daml.Ledger.Client.Reactive.Util;

    public class ServerStreamHelperTest
    {
        const int SequenceMillisDelay = 1050;
        const int DelayTolerance = 20;
        const double DelayTolerancePercent = 0.01;
        const int StreamLength = 10;

        private readonly ITestOutputHelper _output;

        public ServerStreamHelperTest(ITestOutputHelper output)
        {
            _output = output;
        }
 
        [Fact]
        public void SubscriptionToAsyncObservableDoesNotBlockByDefault()
        {
            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable();

            var observer = new TestUtils.TestObserver<int>();

            DateTime before = DateTime.UtcNow;
            DateTime after;

            using (observable.Subscribe(observer))
            {
                after = DateTime.UtcNow;
                observer.WaitForCompletion();
            }

            int delay = (int)(after - before).TotalMilliseconds;

            delay.Should().BeInRange(0, DelayTolerance, "Subscribe method blocked");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ObservationsFromAsyncObservableAreOnTheTaskPoolSchedulerByDefault()
        {
            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable();
            var observer = new TestUtils.TestObserver<int>();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            observer.NotificationThreadIds.Should().NotContain(Thread.CurrentThread.ManagedThreadId, "Unexpectedly notified on main thread");
            observer.NotificationTaskIds.Should().HaveCountGreaterOrEqualTo(1);
            observer.NotificationTaskIds.Should().NotContainNulls("Notifications were not using the Task pool");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CanSpecifySchedulerForSubscriptionForAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable(scheduler);

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            scheduleThread.Should().NotBeNull("Subscription scheduler not used");
            observer.NotificationThreadIds.Should().ContainSingle("Unexpected number of notification threads");
            observer.NotificationThreadIds.Should().Contain(scheduleThread.ManagedThreadId, "Not notified on the scheduler thread");
            observer.NotificationTaskIds.Should().ContainSingle("Unexpected Task notifier count");
            observer.NotificationTaskIds.Should().Contain((int?) null, "Unexpectedly notified by a Task");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CanSpecifySchedulerForObservationsOnAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable().ObserveOn(scheduler);
            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            observer.NotificationThreadIds.Should().ContainSingle("Notified on more than one thread");
            observer.NotificationThreadIds.Should().Contain(scheduleThread.ManagedThreadId, "Not notified on specified scheduler thread");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CanSpecifySynchronizationContextForObservationsOnAsyncObservable()
        {
            var synchronizationContext = new TestSynchronizationContext();

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable().ObserveOn(synchronizationContext);
            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            synchronizationContext.NotifiedCount.Should().Be(StreamLength);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CannotOverrideSchedulerForSubscriptionOnAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable();

            observable.SubscribeOn(scheduler);                           // 2nd call to SubscribeOn does nothing...

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            scheduleThread.Should().BeNull("Overridden scheduler was used");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ObservationsUseTheCurrentThreadIfImmediateSchedulerSpecifiedForSubscriptionOnAsyncObservable()
        {
            CheckNotificationsOnMainThread(CreateIntAsyncEnumerable().CreateAsyncObservable(Scheduler.Immediate));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void ObservationsOnSyncObservableUseTheCurrentThread()
        {
            CheckNotificationsOnMainThread(CreateIntAsyncEnumerable().CreateSyncObservable());
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CanSpecifySchedulerForSubscriptionOnSyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateSyncObservable().SubscribeOn(scheduler);

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            scheduleThread.Should().NotBeNull("Specified scheduler was not used");

            observer.NotificationThreadIds.Should().ContainSingle("Notified on more than one thread");
            observer.NotificationThreadIds.Should().Contain(scheduleThread.ManagedThreadId, "Not notified on schedule thread");
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void MappedAsyncObservablesMaintainAsynchronicity()
        {
            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable().Select(v => (double) v);

            var observer = new TestUtils.TestObserver<double>(StreamLength, SequenceMillisDelay, DelayTolerance);

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();    // WaitForCompletion will do tests...
        }

        private IAsyncEnumerable<int> CreateIntAsyncEnumerable()
        {
            return TestUtils.CreateAsyncStream(1, v => v <= StreamLength, v => ++v, SequenceMillisDelay);
        }

        /// <summary>
        /// Create an Int observer that checks itself on completion
        /// </summary>
        /// <returns></returns>
        private TestUtils.TestObserver<int> CreateIntAsyncObserver()
        {
            return new TestUtils.TestObserver<int>(StreamLength, SequenceMillisDelay, DelayTolerance);
        }

        private void CheckNotificationsOnMainThread(IObservable<int> observable)
        {
            var observer = new TestUtils.TestObserver<int>();

            DateTime before = DateTime.UtcNow;
            DateTime after;

            using (observable.Subscribe(observer))
            {
                after = DateTime.UtcNow;
                observer.WaitForCompletion();
            }

            int delay = (int)(after - before).TotalMilliseconds;

            var expectedMinimumDelay = SequenceMillisDelay * StreamLength;
            var expectedMaximumDelay = (int) (expectedMinimumDelay + (expectedMinimumDelay * DelayTolerancePercent));

            delay.Should().BeInRange(expectedMinimumDelay, expectedMaximumDelay, "Subscribe method blocked");

            observer.NotificationThreadIds.Should().ContainSingle("Notified on more than one thread");
            observer.NotificationThreadIds.Should().Contain(Thread.CurrentThread.ManagedThreadId, "Not notified on main thread");
        }
    }
}