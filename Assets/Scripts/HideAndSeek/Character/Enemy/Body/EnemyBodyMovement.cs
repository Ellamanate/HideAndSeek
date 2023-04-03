using UnityEngine;
using UnityEngine.AI;

namespace HideAndSeek
{
    public class EnemyBodyMovement : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        
        public bool IsStopped => NavMeshAgent.isStopped;
        public bool PathPending => NavMeshAgent.pathPending;
        public bool HasPath => NavMeshAgent.hasPath;
        public float StoppingDistance => NavMeshAgent.stoppingDistance;
        public float RemainingDistance => NavMeshAgent.remainingDistance;
        public float Acceleration => NavMeshAgent.acceleration;
        public Vector3 Velocity => NavMeshAgent.velocity;

        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        public bool Warp(Vector3 position) => NavMeshAgent.Warp(position);
        public void MoveTo(Vector3 destination) => NavMeshAgent.SetDestination(destination);
        public void SetMaxSpeed(float speed) => NavMeshAgent.speed = speed;
        public void Stop() => NavMeshAgent.ResetPath();
    }
}