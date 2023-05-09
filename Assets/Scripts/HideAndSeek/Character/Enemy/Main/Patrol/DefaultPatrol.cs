namespace HideAndSeek
{
    public class DefaultPatrol : BasePatrol
    {
        private readonly EnemyPatrolPointsSet _patrolSet;

        public DefaultPatrol(EnemyModel model, EnemyBody body, EnemyPatrolPointsSet patrolSet)
            : base(model, body)
        {
            _patrolSet = patrolSet;
            _patrolSet.TryGetPatrolPoints(PatrolQueue.First, out PatrolPoints);
        }

        public void SetNextQueue(PatrolQueue queue)
        {
            _patrolSet.TryGetPatrolPoints(queue, out PatrolPoints);
            CurrentPatrolIndex = 0;
        }
    }
}
