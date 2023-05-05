using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class SceneInteractions
    {
        private readonly IInteractable<Enemy>[] _interactionsForEnemy;

        public SceneInteractions(IInteractable<Enemy>[] interactionsForEnemy)
        {
            _interactionsForEnemy = interactionsForEnemy;
        }

        public bool TryGetPriorityInteractionNear(Enemy enemy, out IInteractable<Enemy> interactable)
        {
            interactable = _interactionsForEnemy
                .Where(x => InNearEnemyValid(enemy, x))
                .FirstOrDefault();

            return interactable != null;
        }

        private bool InNearEnemyValid(Enemy enemy, IInteractable<Enemy> interactable)
        {
            return CanInteract() && interactable is IPositionedInteraction positioned && InteractionNear();

            bool CanInteract() => enemy.Interact.IsInteractableValid(interactable);
            bool InteractionNear() => Vector3.Distance(enemy.Model.Position, positioned.Position) <= enemy.Model.MaxDistanceToInteractable;
        }
    }
}
