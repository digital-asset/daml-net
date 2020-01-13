// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Daml.Ledger.Client.Reactive.Test
{
    public static class TestUtils
    {
        public static IAsyncEnumerable<T> CreateAsyncStream<T>(T initialState, Func<T, bool> condition, Func<T, T> iterate, int millisDelay)
        {
            return AsyncEnumerable.Generate(initialState, condition, v =>
            {
                Thread.Sleep(millisDelay);
                return iterate(v);
            }, v => v);
        }

        public class TestObserver<T> : IObserver<T>
        {
            public TestObserver()
            {
                Values = new List<T>();
                Delays = new List<int>();
                NotificationThreadIds = new HashSet<int>();
                NotificationTaskIds = new HashSet<int?>();
            }

            public TestObserver(int streamLengthToCheck, int sequenceMillisDelayToCheck, int delayToleranceToCheck)
              : this()
            {
                _streamLengthToCheck = streamLengthToCheck;
                _sequenceMillisDelayToCheck = sequenceMillisDelayToCheck;
                _delayTolerance = delayToleranceToCheck;
            }

            public List<T> Values { get; }
            public List<int> Delays { get; }

            public HashSet<int> NotificationThreadIds { get; }
            public HashSet<int?> NotificationTaskIds { get; }

            public void OnCompleted()
            {
                _completedEvent.Set();
            }

            public void OnError(Exception error)
            {
                Assert.Fail($"OnError received {error.Message}");
                _completedEvent.Set();
            }

            public void OnNext(T value)
            {
                DateTime now = DateTime.UtcNow;

                Values.Add(value);

                if (_previous != DateTime.MinValue)
                    Delays.Add((int)(now - _previous).TotalMilliseconds);

                NotificationThreadIds.Add(Thread.CurrentThread.ManagedThreadId);
                NotificationTaskIds.Add(Task.CurrentId);

                _previous = now;
            }

            public void WaitForCompletion()
            {
                _completedEvent.WaitOne();

                if (_streamLengthToCheck.HasValue)
                    Assert.AreEqual(_streamLengthToCheck.Value, Values.Count, "Notification count not as expected");

                if (_sequenceMillisDelayToCheck.HasValue && _delayTolerance.HasValue)
                    foreach (var delay in Delays)
                        Assert.AreEqual(_sequenceMillisDelayToCheck.Value, delay, _delayTolerance.Value, "Notification delay different then expected");
            }

            private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);
            private DateTime _previous = DateTime.MinValue;

            private readonly int? _streamLengthToCheck;
            private readonly int? _sequenceMillisDelayToCheck;
            private readonly int? _delayTolerance;
        }
    }

    public class TestSynchronizationContext : SynchronizationContext
    {
        public TestSynchronizationContext()
        : base()
        {
            _thread = new Thread(Process) { IsBackground = true };
            _thread.Start();
        }

        public int ContextThreadId => _thread.ManagedThreadId;
        public int NotifiedCount { get; private set; }

        public override void Send(SendOrPostCallback d, object state)
        {
            using (var waitEvent = new ManualResetEventSlim())
            {
                _queue.Add((s => { d(s); waitEvent.Set(); }, state));
                waitEvent.Wait();
            }
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            _queue.Add((d, state));
        }

        private void Process()
        {
            SetSynchronizationContext(this);

            // These callbacks will actually execute on the task pool - forcing them onto our thread is too complicated to solve here !
            foreach (var item in _queue.GetConsumingEnumerable())
            {
                item.callback(item.state);
                if (item.state != null)
                    ++NotifiedCount;
            }
        }

        private readonly BlockingCollection<(SendOrPostCallback callback, object state)> _queue = new BlockingCollection<(SendOrPostCallback, object)>();
        private readonly Thread _thread;
    }
}



