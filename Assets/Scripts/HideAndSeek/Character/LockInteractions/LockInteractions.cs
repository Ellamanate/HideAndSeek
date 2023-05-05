using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HideAndSeek
{
    public class LockInteractions<T> : IDisposable
    {
        private CancellationTokenSource _token;
        private List<IInteractable<T>> _locked;
        private Dictionary<IInteractable<T>, TimeLock> _lockedByTime;

        public LockInteractions()
        {
            _token = new CancellationTokenSource();
            _locked = new List<IInteractable<T>>();
            _lockedByTime = new Dictionary<IInteractable<T>, TimeLock>();
        }

        public bool IsLocked(IInteractable<T> interactable)
        {
            return _locked.Contains(interactable);
        }

        public void LockInteractionByTime(IInteractable<T> interactable, float time)
        {
            if (_lockedByTime.TryGetValue(interactable, out var timeLock))
            {
                _ = TimeUnlock(interactable, timeLock, time, _token.Token);
            }
            else
            {
                timeLock = new TimeLock();
                _lockedByTime[interactable] = timeLock;
                _locked.Add(interactable);
                _ = TimeUnlock(interactable, timeLock, time, _token.Token);
            }
        }

        public void Clear()
        {
            foreach (var timeLock in _lockedByTime.Values)
            {
                timeLock.Dispose();
            }

            _lockedByTime.Clear();
            _locked.Clear();
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
            Clear();
        }

        private async UniTask TimeUnlock(IInteractable<T> interactable, TimeLock timeLock, float time, CancellationToken token)
        {
            await timeLock.WaitUnlock(time, token);

            timeLock.Dispose();
            _lockedByTime.Remove(interactable);
            _locked.Remove(interactable);
        }
    }
}
