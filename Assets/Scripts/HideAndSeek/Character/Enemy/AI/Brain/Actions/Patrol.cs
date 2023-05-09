using UnityEngine;
using Zenject;

namespace HideAndSeek.AI
{
    public class Patrol : BaseAction
    {
        private readonly EnemyPatrol _patrol;
        private readonly EnemyMovement _movement;

        public Patrol(EnemyPatrol patrol, EnemyMovement movement)
        {
            _patrol = patrol;
            _movement = movement;
        }

        public override void Execute()
        {
            _patrol.SetDefaultState();
            Vector3 patrolPointPosition = _patrol.GetCurrentPatrolPosition();
            _movement.MoveTo(patrolPointPosition);
        }

        public class Factory : PlaceholderFactory<Patrol> { }
    }
}
