using UnityEngine;

namespace HideAndSeek
{
    public class MainCamera
    {
        private readonly MoveCamera _moveCamera;

        private Transform _playerTransform;

        public MainCamera(MoveCamera moveCamera)
        {
            _moveCamera = moveCamera;
        }

        public void SetPlayer(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _moveCamera.SetTarget(_playerTransform);
            _moveCamera.SetDampingX(0);
        }

        public void LookAtShelter(Shelter shelter)
        {
            _moveCamera.SetTarget(shelter.transform);
            _moveCamera.SetDampingX(0.5f);
        }

        public void LookAtPlayer()
        {
            if (_playerTransform != null)
            {
                _moveCamera.SetTarget(_playerTransform);
                _moveCamera.SetDampingX(0);
            }
        }
    }
}
