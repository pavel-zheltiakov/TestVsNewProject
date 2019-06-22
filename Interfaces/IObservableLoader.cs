using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace VSNewProjectDialogExample.Interfaces
{
    public interface IObservableLoader<TResult>
    {
        ISubject<bool> LoadStarted { get; }
        ISubject<TResult> LoadCompleted { get; }
        Task<TResult> Load();
    }

    public class ObservableLoader<TResult> : IObservableLoader<TResult>
    {
        private readonly Func<TResult> _loadFunc;

        public ObservableLoader(Func<TResult> loadFunc)
        {
            _loadFunc = loadFunc;
            LoadStarted = new Subject<bool>();
            LoadCompleted = new Subject<TResult>();
        }

        public ISubject<bool> LoadStarted { get; }
        public ISubject<TResult> LoadCompleted { get; }

        public Task<TResult> Load()
        {
            LoadStarted.OnNext(true);

            return Task<TResult>.Factory.StartNew(() => _loadFunc())
                .ContinueWith(r =>
                {
                    if (!r.IsFaulted)
                        LoadCompleted.OnNext(r.Result);
                    else
                    {
                        LoadCompleted.OnError(r.Exception);
                    }

                    return r.Result;
                });
        }
    }
}