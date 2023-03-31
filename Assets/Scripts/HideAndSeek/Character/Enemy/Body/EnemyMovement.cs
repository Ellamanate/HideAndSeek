using UnityEngine;
using UnityEngine.AI;

namespace HideAndSeek
{
    public class EnemyMovement : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        public bool Warp(Vector3 position) => NavMeshAgent.Warp(position);
        public void MoveTo(Vector3 position) => NavMeshAgent.SetDestination(position);
        public void SetSpeed(float speed) => NavMeshAgent.speed = speed;
        public void Stop() => NavMeshAgent.ResetPath();
    }
}