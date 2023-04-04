using UnityEngine;

namespace HideAndSeek
{
    public class PlayerTrigger : BaseTrigger<PlayerBody>
    {
        protected override bool TryGetComponent(Collider other, out PlayerBody component)
        {
            var body = other.attachedRigidbody;

            if (body != null && body.TryGetComponent(out component))
            {
                return true;
            }

            component = null;
            return false;
        }
    }
}