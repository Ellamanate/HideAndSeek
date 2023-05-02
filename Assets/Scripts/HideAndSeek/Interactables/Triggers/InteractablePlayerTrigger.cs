using UnityEngine;

namespace HideAndSeek
{
    public class InteractablePlayerTrigger : BaseTrigger<IInteractableForPlayer>
    {
        protected sealed override bool TryGetComponent(Collider other, out IInteractableForPlayer component)
            => other.TryGetComponent(out component);
    }
}
