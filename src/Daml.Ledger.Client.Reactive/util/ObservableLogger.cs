// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using log4net;

namespace Daml.Ledger.Client.Reactive.Util
{
    public class ObservableLogger
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ObservableLogger));

        public static IObservable<T> Log<T>(IObservable<T> observable, string name) => _logger.IsDebugEnabled ? new LoggingObservable<T>(observable, name) : observable;

        private class LoggingObservable<T> : IObservable<T>
        {
            public LoggingObservable(IObservable<T> observable, string name)
            {
                _observable = observable;
                _name = name;
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                _logger.DebugFormat("{0}.subscribe: {1}", _name, observer);
                return new LoggingObserver<T>(_observable, observer, _name);
            }

            private readonly IObservable<T> _observable;
            private readonly string _name;
        }

        public class LoggingObserver<T> : IObserver<T>, IDisposable
        {
            public LoggingObserver(IObservable<T> observable, IObserver<T> observer, string observableName)
            {
                _observer = observer;
                _observableName = observableName;

                _subscription = observable.Subscribe(this);
            }

            public void OnCompleted()
            {
                _logger.DebugFormat("{0}.OnCompleted", _observableName);
                _observer.OnCompleted();
            }

            public void OnError(Exception error)
            {
                _logger.Error($"{_observableName}.OnError: {error.Message}");
                _observer.OnError(error);
            }

            public void OnNext(T value)
            {
                _logger.DebugFormat("{0}.OnNext: {1}", _observableName, value);
                _observer.OnNext(value);
            }

            public void Dispose()
            {
                _logger.DebugFormat("{0}.Dispose", _observableName);
                _subscription.Dispose();
            }

            private readonly IObserver<T> _observer;
            private readonly string _observableName;
            private readonly IDisposable _subscription;
        }
    }
}
