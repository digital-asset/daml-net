// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using Daml.Ledger.Client.Reactive.Util;
using NUnit.Framework;

namespace Daml.Ledger.Client.Reactive.Test.Util
{
    [TestFixture, Explicit]  // Tests take 10 seconds or so each...
    public class ServerStreamHelperTest
    {
        const int SequenceMillisDelay = 1050;
        const int DelayTolerance = 20;
        const int StreamLength = 10;

        [Test]
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

            Assert.AreEqual(0, delay, DelayTolerance, "Subscribe method blocked");
        }

        [Test]
        public void ObservationsFromAsyncObservableAreOnTheTaskPoolSchedulerByDefault()
        {
            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable();
            var observer = new TestUtils.TestObserver<int>();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.IsFalse(observer.NotificationThreadIds.Contains(Thread.CurrentThread.ManagedThreadId), "Unexpectedly notified on main thread");
            Assert.IsTrue(observer.NotificationTaskIds.Count >= 1);
            Assert.IsFalse(observer.NotificationTaskIds.Contains(null), "Notifications were not using the Task pool");
        }

        [Test]
        public void CanSpecifySchedulerForSubscriptionForAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable(scheduler);

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.IsNotNull(scheduleThread, "Subscription scheduler not used");
            Assert.AreEqual(1, observer.NotificationThreadIds.Count, "Unexpected number of notification threads");
            Assert.IsTrue(observer.NotificationThreadIds.Contains(scheduleThread.ManagedThreadId), "Not notified on the scheduler thread");
            Assert.AreEqual(1, observer.NotificationTaskIds.Count, "Unexpected Task notifier count");
            Assert.IsTrue(observer.NotificationTaskIds.Contains(null), "Unexpectedly notified by a Task");
        }

        [Test]
        public void CanSpecifySchedulerForObservationsOnAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable().ObserveOn(scheduler);
            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.AreEqual(1, observer.NotificationThreadIds.Count, "Notified on more than one thread");
            Assert.IsTrue(observer.NotificationThreadIds.Contains(scheduleThread.ManagedThreadId), "Not notified on specified scheduler thread");
        }

        [Test]
        public void CanSpecifySynchronizationContextForObservationsOnAsyncObservable()
        {
            var synchronizationContext = new TestSynchronizationContext();

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable().ObserveOn(synchronizationContext);
            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.AreEqual(StreamLength, synchronizationContext.NotifiedCount);
        }

        [Test]
        public void CannotOverrideSchedulerForSubscriptionOnAsyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateAsyncObservable();

            observable.SubscribeOn(scheduler);                           // 2nd call to SubscribeOn does nothing...

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.IsNull(scheduleThread, "Overridden scheduler was used");
        }

        [Test]
        public void ObservationsUseTheCurrentThreadIfImmediateSchedulerSpecifiedForSubscriptionOnAsyncObservable()
        {
            CheckNotificationsOnMainThread(CreateIntAsyncEnumerable().CreateAsyncObservable(Scheduler.Immediate));
        }

        [Test]
        public void ObservationsOnSyncObservableUseTheCurrentThread()
        {
            CheckNotificationsOnMainThread(CreateIntAsyncEnumerable().CreateSyncObservable());
        }

        [Test]
        public void CanSpecifySchedulerForSubscriptionOnSyncObservable()
        {
            Thread scheduleThread = null;

            IScheduler scheduler = new EventLoopScheduler(f => scheduleThread = new Thread(f) { IsBackground = true });

            var observable = CreateIntAsyncEnumerable().CreateSyncObservable().SubscribeOn(scheduler);

            var observer = CreateIntAsyncObserver();

            using (observable.Subscribe(observer))
                observer.WaitForCompletion();

            Assert.IsNotNull(scheduleThread, "Specified scheduler was not used");

            Assert.AreEqual(1, observer.NotificationThreadIds.Count, "Notified on more than one thread");
            Assert.IsTrue(observer.NotificationThreadIds.Contains(scheduleThread.ManagedThreadId), "Not notified on schedule thread");
        }

        [Test]
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

            Assert.AreEqual(SequenceMillisDelay * StreamLength, delay, DelayTolerance * 2, "Subscribe method blocked");

            Assert.AreEqual(1, observer.NotificationThreadIds.Count, "Notified on more than one thread");
            Assert.IsTrue(observer.NotificationThreadIds.Contains(Thread.CurrentThread.ManagedThreadId), "Not notified on main thread");
        }

    }
}