using UnityEngine;

namespace HideAndSeek
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;
        [SerializeField] private float _speed;

        public void SetVelocity(Vector3 velocity)
        {
            _body.velocity = velocity;
        }
    }
}