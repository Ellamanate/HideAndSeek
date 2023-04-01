using UnityEngine;

namespace HideAndSeek
{
    public interface ITransformable
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}
