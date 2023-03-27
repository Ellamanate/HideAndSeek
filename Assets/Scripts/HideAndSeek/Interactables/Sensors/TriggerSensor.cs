using UnityEngine;

namespace HideAndSeek
{
    public class TriggerSensor : BaseSensor<ITrigger>
    {
        protected sealed override bool TryGetComponent(Collider other, out ITrigger component) 
            => other.TryGetComponent(out component);
    }
}
