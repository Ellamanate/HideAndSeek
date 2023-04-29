using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class SceneInteractions
    {
        private readonly IInteractableForEnemy[] _interactionsForEnemy;

        public SceneInteractions(IInteractableForEnemy[] interactionsForEnemy)
        {
            _interactionsForEnemy = interactionsForEnemy;
        }

        public bool TryGetPriorityInteractionNear(Enemy enemy, out IInteractableForEnemy interactable)
        {
            interactable = _interactionsForEnemy
                .Where(x => x.CanEnemyInteract && Vector3.Distance(enemy.Model.Position, x.Position) <= enemy.Model.MaxDistanceToInteractable)
                .FirstOrDefault();

            return interactable != null;
        }
    }
}
