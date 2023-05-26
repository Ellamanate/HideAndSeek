using UnityEngine;

namespace HideAndSeek
{
    public abstract class EventInvokeRule : ScriptableObject
    {
        public abstract bool CanInvoke(params IEvent[] events);
    }
}
