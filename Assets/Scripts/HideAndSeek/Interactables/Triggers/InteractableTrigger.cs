using UnityEngine;

namespace HideAndSeek
{
    public class InteractableTrigger : BaseTrigger<IInteractable>
    {
        protected sealed override bool TryGetComponent(Collider other, out IInteractable component) 
            => other.TryGetComponent(out component);
    }
}
