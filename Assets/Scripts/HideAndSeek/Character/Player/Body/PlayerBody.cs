using System;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerBody : MonoBehaviour, IDestroyable
    {
        public event Action OnDestroyed;

        [SerializeField] private Rigidbody _body;

        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public InteractableTrigger InteractableTrigger { get; private set; }

        public bool HittedBody(RaycastHit hit)
        {
            return hit.collider.attachedRigidbody == _body;
        }

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