using System;
using Zenject;

namespace HideAndSeek.Utils
{
    public class SubContainerDisposables : IDisposable
    {
        private IDisposable[] _disposables;

        public SubContainerDisposables([Inject(Source = InjectSources.Local)] IDisposable[] disposables)
        {
            _disposables = disposables;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}