using Cinemachine;
using UnityEngine;

namespace HideAndSeek
{
    public class MoveCamera
    {
        private CinemachineVirtualCamera _virtualCamera;

        public MoveCamera(GameSceneReferences references)
        {
            _virtualCamera = references.VirtualCamera;
        }

        public void SetTarget(Transform target)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;
        }
    }
}
