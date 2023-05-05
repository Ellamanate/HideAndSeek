using UnityEngine;

namespace HideAndSeek
{
    public class InteractableEnemyTrigger : BaseTrigger<IInteractable<Enemy>>
    {
        protected sealed override bool TryGetComponent(Collider other, out IInteractable<Enemy> component) 
            => other.TryGetComponent(out component);
    }
}
