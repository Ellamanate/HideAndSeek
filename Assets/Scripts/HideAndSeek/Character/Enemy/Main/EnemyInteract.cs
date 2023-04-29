using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek
{
    public class EnemyInteract
    {
        private readonly MainGame _mainGame;
        private readonly HidePlayer _hidePlayer;
        private readonly EnemyUpdateBrain _enemyUpdateBrain;

        private List<IInteractableForEnemy> _interactables;

        public EnemyInteract(MainGame mainGame, HidePlayer hidePlayer, EnemyUpdateBrain enemyUpdateBrain)
        {
            _mainGame = mainGame;
            _hidePlayer = hidePlayer;
            _enemyUpdateBrain = enemyUpdateBrain;
            _interactables = new List<IInteractableForEnemy>();
        }

        public void Clear()
        {
            _interactables.Clear();
        }

        public bool Contains(IInteractableForEnemy interactable)
        {
            return _interactables.Contains(interactable);
        }

        public bool CanInteract()
        {
            return _interactables.Count > 0 && _interactables.FirstOrDefault(x => x.CanEnemyInteract) != null;
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

                _interactables[0].Interact(enemy);
                _enemyUpdateBrain.UpdateAction();
            }
        }

        public void AddInteractable(Enemy enemy, IInteractableForEnemy interactable)
        {
            if (interactable.TouchTrigger)
            {
                interactable.Interact(enemy);
            }
            else if (!_interactables.Contains(interactable))
            {
                _interactables.Add(interactable);
            }
        }

        public void RemoveInteractable(IInteractableForEnemy interactable)
        {
            _interactables.Remove(interactable);
        }

        public void TouchPlayer(PlayerBody body)
        {
            _mainGame.FailGame();
        }
    }
}
