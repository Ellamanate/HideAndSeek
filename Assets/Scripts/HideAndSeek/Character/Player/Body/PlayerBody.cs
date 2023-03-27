using System;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerBody : MonoBehaviour, IDestroyable
    {
        public event Action OnDestroyed;

        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public TriggerSensor TriggerSensor { get; private set; }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}