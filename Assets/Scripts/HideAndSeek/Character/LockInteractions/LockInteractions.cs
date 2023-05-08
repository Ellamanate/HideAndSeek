using HideAndSeek.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HideAndSeek
{
    public class LockInteractions<T> : IDisposable
    {
        private CancellationTokenSource _token;
        private Dictionary<IInteractable<T>, InteractionLocker> _interactables;
        private bool _disposed;

        public LockInteractions()
        {
            _token = new CancellationTokenSource();
            _interactables = new Dictionary<IInteractable<T>, InteractionLocker>();
        }

        public bool IsLocked(IInteractable<T> interactable)
        {
            return _interactables.TryGetValue(interactable, out var locker) && locker.Locked;
        }

        public void LockInteractionByTime(IInteractable<T> interactable, float time)
        {
            if (_disposed) return;

            if (_interactables.TryGetValue(interactable, out var locker))
            {
                _ = locker.LockByTime(time, _token.Token);
            }
            else
            {
                locker = new InteractionLocker();
                _ = locker.LockByTime(time, _token.Token);

                _interactables[interactable] = locker;
            }
        }

        public void LockByRepetitions(IInteractable<T> interactable, int maxRepetitions)
        {
            if (_disposed) return;

            if (_interactables.TryGetValue(interactable, out var locker))
            {
                locker.LockByRepetitions(maxRepetitions);
            }
            else
            {
                locker = new InteractionLocker();
                locker.LockByRepetitions(maxRepetitions);

                _interactables[interactable] = locker;
            }
        }

        public void Clear()
        {
            if (_disposed) return;

            foreach (var locker in _interactables.Values)
            {
                locker.Dispose();
            }

            _interactables.Clear();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _token.CancelAndDispose();
                Clear();
            }

            _disposed = true;
        }
    }
}
