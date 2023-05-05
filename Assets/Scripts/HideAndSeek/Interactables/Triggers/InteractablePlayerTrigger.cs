using UnityEngine;

namespace HideAndSeek
{
    public class InteractablePlayerTrigger : BaseTrigger<IInteractable<Player>>
    {
        protected sealed override bool TryGetComponent(Collider other, out IInteractable<Player> component)
            => other.TryGetComponent(out component);
    }
}
