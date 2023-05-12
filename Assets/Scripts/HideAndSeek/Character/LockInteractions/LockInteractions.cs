using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HideAndSeek
{
    public class LockInteractions<T> : IDisposable
    {
        private Dictionary<IInteractable<T>, InteractionLocker> _interactables;
        private bool _disposed;

        public LockInteractions()
        {
            _interactables = new Dictionary<IInteractable<T>, InteractionLocker>();
        }

        public bool IsLocked(IInteractable<T> interactable)
        {
            return _interactables.TryGetValue(interactable, out var locker) && locker.Locked;
        }

        public async UniTask LockInteractionByTime(IInteractable<T> interactable, float time, CancellationToken token)
        {
            if (!_disposed)
            {
                if (_interactables.TryGetValue(interactable, out var locker))
                {
                    await locker.LockByTime(time, token);
                }
                else
                {
                    locker = new InteractionLocker();
                    _interactables[interactable] = locker;

                    await locker.LockByTime(time, token);
                }
            }
        }

        public void LockByRepetitions(IInteractable<T> interactable, int maxRepetitions)
        {
            if (!_disposed)
            {
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
        }

        public void Clear()
        {
            if (!_disposed)
            {
                foreach (var locker in _interactables.Values)
                {
                    locker.Clear();
                }

                _interactables.Clear();
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                foreach (var locker in _interactables.Values)
                {
                    locker.Dispose();
                }

                _interactables.Clear();
            }
        }
    }
}
