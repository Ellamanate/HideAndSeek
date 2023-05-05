using System;
using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek
{
    public class EnemyInteract : IDisposable
    {
        private readonly FailGame _failGame;
        private readonly HidePlayer _hidePlayer;
        private readonly EnemyUpdateBrain _enemyUpdateBrain;

        private LockInteractions<Enemy> _lockInteractions;
        private List<IInteractable<Enemy>> _interactables;

        public EnemyInteract(FailGame failGame, HidePlayer hidePlayer, EnemyUpdateBrain enemyUpdateBrain)
        {
            _failGame = failGame;
            _hidePlayer = hidePlayer;
            _enemyUpdateBrain = enemyUpdateBrain;
            _lockInteractions = new LockInteractions<Enemy>();
            _interactables = new List<IInteractable<Enemy>>();
        }

        public void Dispose()
        {
            _lockInteractions.Dispose();
        }

        public void Clear()
        {
            _interactables.Clear();
            _lockInteractions.Clear();
        }

        public bool Contains(IInteractable<Enemy> interactable)
        {
            return _interactables.Contains(interactable);
        }

        public bool CanInteract()
        {
            return _interactables.Count > 0 && GetValidInteraction() != null;
        }

        public bool IsInteractableValid(IInteractable<Enemy> interactable)
        {
            return interactable.LimitInteract.CanEnemyInteract && !_lockInteractions.IsLocked(interactable);
        }

        public void Interact(Enemy enemy)
        {
            if (_interactables.Count > 0)
            {
                if (enemy.Model.PlayerDetectedInShelter)
                {
                    var playersShelter = _interactables.FirstOrDefault(x => Equals(x, _hidePlayer.CurrentShelter));

                    if (playersShelter != null)
                    {
                        playersShelter.Interact(enemy);
                        return;
                    }
                }

                InteractAndLock(enemy, GetValidInteraction());
                _enemyUpdateBrain.UpdateAction();
            }
        }

        public void AddInteractable(Enemy enemy, IInteractable<Enemy> interactable)
        {
            if (interactable.TouchTrigger)
            {
                InteractAndLock(enemy, interactable);
            }
            else if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
            }
        }

        public void RemoveInteractable(IInteractable<Enemy> interactable)
        {
            _interactables.Remove(interactable);
        }

        public void TouchPlayer(PlayerBody body)
        {
            _failGame.SetFail();
        }

        private void InteractAndLock(Enemy enemy, IInteractable<Enemy> interactable)
        {
            if (interactable != null)
            {
                interactable.Interact(enemy);

                if (interactable is ILimitingReuseTime limitingReuseTime)
                {
                    var rule = limitingReuseTime.TimeReuseRule;

                    if (rule.InteractorType.HasFlag(InteractorType.Enemy))
                    {
                        _lockInteractions.LockInteractionByTime(interactable, rule.TimeToReuse);
                    }
                }
            }
        }

        private IInteractable<Enemy> GetValidInteraction()
        {
            return _interactables.FirstOrDefault(IsInteractableValid);
        }
    }
}
