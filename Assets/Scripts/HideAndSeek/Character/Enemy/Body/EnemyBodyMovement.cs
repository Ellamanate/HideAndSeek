using UnityEngine;
using UnityEngine.AI;

namespace HideAndSeek
{
    public class EnemyBodyMovement : MonoBehaviour
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        public bool IsStopped => NavMeshAgent.isStopped;
        public bool PathPending => NavMeshAgent.pathPending;
        public bool HasPath => NavMeshAgent.hasPath;
        public float StoppingDistance => NavMeshAgent.stoppingDistance;
        public float RemainingDistance => NavMeshAgent.remainingDistance;
        public float Acceleration => NavMeshAgent.acceleration;
        public Vector3 Velocity => NavMeshAgent.velocity;

        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        public bool Warp(Vector3 position) => NavMeshAgent.Warp(position);
        public void SetMaxSpeed(float speed) => NavMeshAgent.speed = speed;

        public void MoveTo(Vector3 destination)
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.SetDestination(destination);
        }

        public void Stop()
        {
            NavMeshAgent.velocity = Vector3.zero;
            NavMeshAgent.isStopped = true;
            NavMeshAgent.ResetPath();
        }
    }
}