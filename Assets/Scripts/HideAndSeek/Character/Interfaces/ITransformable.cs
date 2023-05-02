using UnityEngine;

namespace HideAndSeek
{
    public interface ITransformable : IPositioned
    {
        public void SetPosition(Vector3 position);
        public void SetRotation(Quaternion rotation);
    }
}
