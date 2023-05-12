using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public abstract class BasePatrol 
    {
        protected readonly EnemyModel Model;
        protected readonly EnemyBody Body;

        protected PatrolPoint[] PatrolPoints;
        protected int CurrentPatrolIndex;

        public BasePatrol(EnemyModel model, EnemyBody body)
        {
            Model = model;
            Body = body;
        }

        public bool StandsAtPatrolPoint()
        {
            var currentPatrolPoint = GetCurrentPatrolPoint();
            return Vector3.Distance(Model.Position, currentPatrolPoint.transform.position) < Body.Movement.StoppingDistance * 2;
        }

        public PatrolPoint GetCurrentPatrolPoint()
        {
            return PatrolPoints[CurrentPatrolIndex];
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return PatrolPoints.Any(x => x.transform.position == position);
        }

        public void DropPatrolPoints()
        {
            CurrentPatrolIndex = 0;
        }

        public virtual void SetNextPoint()
        {
            CurrentPatrolIndex++;

            if (CurrentPatrolIndex >= PatrolPoints.Length)
            {
                CurrentPatrolIndex = 0;
            }
        }
    }
}
