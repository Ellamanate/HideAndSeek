using UnityEngine;

namespace HideAndSeek
{
    public interface IPositioned
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
    }
}
