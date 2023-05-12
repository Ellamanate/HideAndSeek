using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HideAndSeek
{
    public abstract class BaseInteract<T> : IDisposable
    {
        protected List<IInteractable<T>> Interactables;

        private LockInteractions<T> _lockInteractions;
        private CancellationTokenSource _token;

        public BaseInteract()
        {
            _lockInteractions = new LockInteractions<T>();
            Interactables = new List<IInteractable<T>>();
            _token = new CancellationTokenSource();
        }

        protected abstract InteractorType CurrentInteractorType { get; }

        public bool Contains(IInteractable<T> interactable)
        {
            return Interactables.Contains(interactable);
        }

        public bool CanInteract()
        {
            return Interactables.Count > 0 && GetValidInteraction() != null;
        }

        public bool IsInteractableValid(IInteractable<T> interactable)
        {
            return CheckInteractionAvailable(interactable) && !_lockInteractions.IsLocked(interactable);
        }

        public void Dispose()
        {
            _lockInteractions.Dispose();
            _token.CancelAndDispose();

            OnDisposed();
        }

        public void Clear()
        {
            Interactables.Clear();
            _lockInteractions.Clear();
            _token = _token.Refresh();
        }

        public void AddInteractable(T agent, IInteractable<T> interactable)
        {
            if (interactable.TouchTrigger)
            {
                InteractAndLock(agent, interactable);
            }
            else if (!Interactables.Contains(interactable))
            {
                Interactables.Add(interactable);
                OnInteractableAdded();
            }
        }

        public void RemoveInteractable(IInteractable<T> interactable)
        {
            Interactables.Remove(interactable);
            OnInteractableRemoved();
        }

        public abstract void Interact(T agent);
        public abstract bool CheckInteractionAvailable(IInteractable<T> interactable);

        protected virtual void OnInteractableAdded() { }
        protected virtual void OnInteractableRemoved() { }
        protected virtual void OnLockTimeEnded() { }
        protected virtual void OnDisposed() { }

        protected IInteractable<T> GetValidInteraction()
        {
            return Interactables.FirstOrDefault(IsInteractableValid);
        }

        protected void InteractAndLock(T agent, IInteractable<T> interactable)
        {
            if (interactable != null)
            {
                interactable.Interact(agent);
                LockInteraction(interactable);
            }
        }

        private void LockInteraction(IInteractable<T> interactable)
        {
            if (interactable is ILimitingReuseTime limitingReuseTime)
            {
                var rule = limitingReuseTime.ReuseTimeRule;

                if (rule.InteractorType.HasFlag(CurrentInteractorType))
                {
                    _ = LockByTime(interactable, rule.TimeToReuse);
                }
            }
            else if (interactable is ILimitingReuseAction limitingReuseAction)
            {
                var rule = limitingReuseAction.ReuseActionRule;

                if (!rule.Unlimit && rule.InteractorType.HasFlag(CurrentInteractorType))
                {
                    _lockInteractions.LockByRepetitions(interactable, rule.Limit);
                }
            }
        }

        private async UniTask LockByTime(IInteractable<T> interactable, float time)
        {
            await _lockInteractions.LockInteractionByTime(interactable, time, _token.Token);

            OnLockTimeEnded();
        }
    }
}
