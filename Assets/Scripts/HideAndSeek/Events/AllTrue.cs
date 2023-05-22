using UnityEngine;

namespace HideAndSeek
{
    [CreateAssetMenu(menuName = "EventInvokeRule/AllTrue", fileName = "AllTrue")]
    public class AllTrue : EventInvokeRule
    {
        public override bool CanInvoke(params IEvent[] events)
        {
            bool canInteract = true;

            foreach (var @event in events)
            {
                canInteract = canInteract && @event.CanInvoke();
            }

            return canInteract;
        }
    }
}
