using UnityEngine;

namespace HideAndSeek
{
    public class InteractionHint : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private Transform _camera;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            _target.rotation = Quaternion.Euler(_camera.eulerAngles.x, 0, 0);
        }

        public void SetAcive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetPositionAt(IPositionedInteraction interactable)
        {
            _target.position = interactable.Position + _offset;
        }
    }
}
