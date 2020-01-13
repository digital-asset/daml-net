// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Com.Daml.Ledger.Api.Util
{
    public interface ISingleObserver<T>
    {
        void OnSuccess(T value);
        void OnError(Exception error);
    }

    public interface ISingleSource<T>
    {
        IDisposable Subscribe(ISingleObserver<T> observer);
    }

    /// <summary>
    /// Represents a single observable value. Whereas the Java version has tons of methods similar to Observable, this version
    /// has a minimal set and it should be converted to an Observable in order to do anything more sophisticated.
    /// The Result property will block until completion, as for a Task.
    ///
    /// See http://reactivex.io/RxJava/javadoc/io/reactivex/Single.html
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Single<T> : ISingleSource<T>
    {
        private readonly IObservable<T> _impl;

        /// <summary>
        /// Initialise with a value so this is immediately completed
        /// </summary>
        /// <param name="value"></param>
        public Single(T value)
        {
            var asyncSubject = new AsyncSubject<T>();
            _impl = asyncSubject;
            asyncSubject.OnNext(value);
            asyncSubject.OnCompleted();
        }

        /// <summary>
        /// Initialise with a Task that may not yet be complete
        /// </summary>
        /// <param name="task"></param>
        public Single(Task<T> task)
        {
            _impl = task.ToObservable();
        }

        /// <summary>
        ///  Initialise with an error to provide the observer 
        /// </summary>
        /// <param name="error"></param>
        public Single(Exception error)
        {
            var asyncSubject = new AsyncSubject<T>();
            _impl = asyncSubject;
            asyncSubject.OnError(error);
        }

        public IDisposable Subscribe(ISingleObserver<T> singleObserver) => _impl.Subscribe(new SingleObserverShim(singleObserver));

        /// <summary>
        ///  Block on the result if not completed
        /// </summary>
        public T Result => _impl.ToTask().Result;

        /// <summary>
        /// Return this as an Observable
        /// </summary>
        /// <returns></returns>
        public IObservable<T> ToObservable() => _impl;

        // Shim the notifications from the AsyncSubject or Task-based Observable to the SingleObserver
        private class SingleObserverShim : IObserver<T>
        {
            private readonly ISingleObserver<T> _singleObserver;

            public SingleObserverShim(ISingleObserver<T> singleObserver) { _singleObserver = singleObserver; }

            void IObserver<T>.OnCompleted() { }
            void IObserver<T>.OnError(Exception error) => _singleObserver.OnError(error);
            void IObserver<T>.OnNext(T value) => _singleObserver.OnSuccess(value);
        }
    }

    public static class Single
    {
        public static Single<T> Just<T>(T value) => new Single<T>(value);

        public static Single<T> Just<T>(Task<T> value) => new Single<T>(value);

        public static Single<T> AsSingle<T>(this Task<T> task) => new Single<T>(task);
        public static Single<T> Error<T>(Exception error) => new Single<T>(error);

        public static IDisposable Subscribe<T>(this Single<T> single, Action<T> onSuccess) => single.Subscribe(new SingleObserver<T>(onSuccess, null));
        public static IDisposable Subscribe<T>(this Single<T> single, Action<T> onSuccess, Action<Exception> onError) => single.Subscribe(new SingleObserver<T>(onSuccess, onError));

        private class SingleObserver<T> : ISingleObserver<T>
        {
            private readonly Action<T> _onSuccess;
            private readonly Action<Exception> _onError;

            public SingleObserver(Action<T> onSuccess, Action<Exception> onError) { _onSuccess = onSuccess; _onError = onError; }

            public void OnSuccess(T value) => _onSuccess(value);
            public void OnError(Exception error)
            {
                if (_onError == null)
                    throw error;
                _onError(error);
            }
        }
    }
}
