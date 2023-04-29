using UnityEngine;

namespace HideAndSeek
{
    public class InteractableEnemyTrigger : BaseTrigger<IInteractableForEnemy>
    {
        protected sealed override bool TryGetComponent(Collider other, out IInteractableForEnemy component) 
            => other.TryGetComponent(out component);
    }
}
