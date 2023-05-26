using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;

namespace HideAndSeek
{
    [Serializable]
    public class InvokeEvents : SerializedMonoBehaviour, IEvent
    {
        [SerializeField] private EventInvokeRule _rule;
        [OdinSerialize] private IEvent[] _events;

        public bool CanInvoke()
        {
            return _rule.CanInvoke(_events);
        }

        public void Invoke()
        {
            foreach (var @event in _events)
            {
                @event.Invoke();
            }
        }
    }
}
