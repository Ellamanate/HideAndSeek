using UnityEngine;

namespace HideAndSeek
{
    public class PlayerTrigger : BaseTrigger<PlayerBody>
    {
        protected override bool TryGetComponent(Collider other, out PlayerBody component)
            => other.TryGetComponent(out component);
    }
}