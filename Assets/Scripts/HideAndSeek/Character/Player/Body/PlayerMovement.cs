using Sirenix.OdinInspector;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;

        [ShowInInspector, ReadOnly] private float _speed;

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void MoveToDirection(Vector3 direction)
        {
            _body.velocity = direction * _speed;
        }

        public void SetPosition(Vector3 position)
        {
            _body.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _body.rotation = rotation;
        }
    }
}