using UnityEngine;

namespace HideAndSeek
{
    public class InteractionHint : BaseWorldUI
    {
        [SerializeField] private Vector3 _offset;

        public void SetPosition(Vector3 position)
        {
            Target.position = position + _offset;
        }
    }
}
