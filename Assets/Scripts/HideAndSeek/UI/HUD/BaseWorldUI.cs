using UnityEngine;

namespace HideAndSeek
{
    public class BaseWorldUI : MonoBehaviour
    {
        [SerializeField] protected Transform Target;

        private Transform _camera;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            Target.rotation = Quaternion.Euler(_camera.eulerAngles.x, 0, 0);
        }

        public void SetAcive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
