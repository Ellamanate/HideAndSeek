using System;
using System.Collections.Generic;

namespace HideAndSeek.Utils
{
    public class SceneDisposables : IDisposable
    {
        private List<IDisposable> _disposable;

        public SceneDisposables()
        {
            _disposable = new List<IDisposable>();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposable)
            {
                disposable.Dispose();
            }
        }

        public void AddDisposable(IDisposable disposable)
        {
            _disposable.Add(disposable);
        }
    }
}