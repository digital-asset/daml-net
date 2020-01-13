// Copyright(c) 2019 Digital Asset(Switzerland) GmbH and/or its affiliates.All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Grpc.Core;

namespace Daml.Ledger.Client.Reactive.Util
{
    public static class ServerStreamHelper
    {
        /// <summary>
        /// Create an Observable from the ResponseStream of an AsyncServerStreamingCall - a type that is derived from IAsyncEnumerable so we provide extension methods
        /// for both types.
        ///
        /// NOTE:
        ///
        /// Ordinarily the same thread that calls the Subscribe() method on the Observable then does the work of notifying the stream to the Observer via the OnNext()
        /// method, followed by calling the OnComplete() method to signal the end of the Item stream.
        /// 
        /// However, as this method is dealing with a stream of Items from a server and obtaining them by calling MoveNext on an Iterator in a loop until the stream
        /// is complete (which it may never be for certain streams, like Transactions), then blocking the Subscribe method by default should rarely be the correct
        /// action.
        ///
        /// Therefore this method will return an Observable on which the SubscribeOn() method has been called, with either the specified Scheduler or the TaskPool
        /// Scheduler, so the subscription/notification work is done on a thread different from the one that called the Subscribe() method, which will therefore not
        /// block.   
        ///
        /// Note that once a Scheduler has been specified via the SubscribeOn() method it does not seem possible to override it, so we have provided another method
        /// 'CreateSyncObservable' that does not install a scheduler via SubscribeOn(), in case SubscribeOn() will be called later on it.
        ///
        /// Also note that the default is for the thread/Task from the subscription scheduler to perform the notification via OnNext, but this behaviour can be changed
        /// by specifying another Scheduler to the ObserveOn method on the Observable. When the subscription thread/task calls OnNext then a context switch occurs to
        /// the thread/Task in the ObserveOn scheduler. A SynchronizationContext can also be specified to ObserveOn.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="asyncServerStreamingCall">The pull stream to iterate over and convert into a push stream</param>
        /// <param name="subscriptionScheduler">The scheduler to specify to SubscribeOn, that will run the subscription task, and notification tasks by default</param>
        /// <returns></returns>
        public static IObservable<TResult> CreateAsyncObservable<TResult>(this AsyncServerStreamingCall<TResult> asyncServerStreamingCall, IScheduler subscriptionScheduler = null)
        {
            return CreateObservable(asyncServerStreamingCall.ResponseStream, subscriptionScheduler ?? TaskPoolScheduler.Default);
        }

        public static IObservable<TResult> CreateAsyncObservable<TResult>(this IAsyncEnumerable<TResult> asyncEnumerable, IScheduler subscriptionScheduler = null)
        {
            return CreateObservable(asyncEnumerable.GetEnumerator(), subscriptionScheduler ?? TaskPoolScheduler.Default);
        }

        public static IObservable<TResult> CreateAsyncObservable<TResult>(this IAsyncEnumerator<TResult> asyncEnumerator, IScheduler subscriptionScheduler = null)
        {
            return CreateObservable(asyncEnumerator, subscriptionScheduler ?? TaskPoolScheduler.Default);
        }

        /// <summary>
        /// Create an Observable from an IAsyncEnumerable but don't specify a SubscribeOn scheduler. Therefore if a scheduler is not specified via SubscribeOn
        /// this will cause the Subscribe() call on the Observable to block until the stream received from the server is complete, which might be dangerous
        /// and possibly also naive.  
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="asyncServerStreamingCall">The pull stream to iterate over and convert into a push stream</param>
        /// <returns></returns>
        public static IObservable<TResult> CreateSyncObservable<TResult>(this AsyncServerStreamingCall<TResult> asyncServerStreamingCall)
        {
            return CreateObservable(asyncServerStreamingCall.ResponseStream, null);
        }

        public static IObservable<TResult> CreateSyncObservable<TResult>(this IAsyncEnumerable<TResult> asyncEnumerable)
        {
            return CreateObservable(asyncEnumerable.GetEnumerator(), null);
        }

        private static IObservable<TResult> CreateObservable<TResult>(IAsyncEnumerator<TResult> asyncEnumerator, IScheduler subscriptionScheduler)
        {
            var observer = Observable.Create<TResult>(async obs =>
            {
                try
                {
                    while (await asyncEnumerator.MoveNext())
                        obs.OnNext(asyncEnumerator.Current);

                    obs.OnCompleted();
                }
                catch (Exception ex)
                {
                    obs.OnError(ex);
                }
            });

            return subscriptionScheduler != null ? observer.SubscribeOn(subscriptionScheduler) : observer;
        }
    }
}
