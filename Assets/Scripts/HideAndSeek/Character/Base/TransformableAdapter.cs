using UnityEngine;

namespace HideAndSeek
{
    public class TransformableAdapter : ITransformable
    {
        private readonly Transform _transform;

        public TransformableAdapter(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 Position => _transform.position;
        public Quaternion Rotation => _transform.rotation;

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }
    }
}