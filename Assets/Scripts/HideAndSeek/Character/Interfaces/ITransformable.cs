using UnityEngine;

namespace HideAndSeek
{
    public interface ITransformable
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public void SetPosition(Vector3 position);
        public void SetRotation(Quaternion rotation);
    }
}
