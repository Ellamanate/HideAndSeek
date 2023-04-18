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
        }
    }
}
