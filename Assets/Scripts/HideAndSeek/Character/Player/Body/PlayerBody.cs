using System;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerBody : MonoBehaviour, IDestroyable
    {
        public event Action OnDestroyed;

        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Rigidbody _body;

        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public InteractablePlayerTrigger InteractableTrigger { get; private set; }
        [field: SerializeField] public Transform RaycastPosition { get; private set; }

        public bool HittedBody(RaycastHit hit)
        {
            return hit.collider.attachedRigidbody == _body;
        }

        public void SetVisible(bool visible)
        {
            _renderer.enabled = visible;
            _body.useGravity = visible;
            gameObject.layer = LayerMask.NameToLayer(visible ? "Player" : "InShelter");

            if (!visible)
            {
                _body.velocity = Vector3.zero;
            }
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