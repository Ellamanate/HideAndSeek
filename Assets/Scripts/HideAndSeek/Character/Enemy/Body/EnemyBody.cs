using System;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyBody : MonoBehaviour, IDestroyable, IInteractable
    {
        public event Action OnDestroyed;
        
        [field: SerializeField] public EnemyBodyMovement Movement { get; private set; }
        [field: SerializeField] public InteractableTrigger InteractableTrigger { get; private set; }

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