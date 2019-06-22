using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace VSNewProjectDialogExample.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<TSource> ObserveOnCurrentSyncronizationContext<TSource>(
            this IObservable<TSource> source)
        {
            return source.ObserveOn(new SynchronizationContextScheduler(SynchronizationContext.Current));
        }

        public static IDisposable LoadSubscribe<TSource, TResult>(this IObservable<TSource> source,
            Action<TSource> onLoadBegin,
            Func<TSource, IObservable<TResult>> loader,
            Action<TResult> onLoadCompleted,
            Action<Exception> onLoadCompletedError
            )
        {
            return source
                .ObserveOnCurrentSyncronizationContext()
                .Do(s =>
                {
                    Debug.WriteLine("StartLoading...");
                    onLoadBegin(s);
                })
                .ObserveOn(Scheduler.Default)
                .Select(loader)
                .Do(s => Debug.WriteLine("Completed loading..."))
                .Switch()
                .ObserveOnCurrentSyncronizationContext()
                .Subscribe(
                    onLoadCompleted,
                    e =>
                    {
                        Debug.WriteLine("Sequence error : {0}", e);
                        onLoadCompletedError(e);
                    },
                    () => Debug.WriteLine("Sequence completed.")
                );
        }
    }
}