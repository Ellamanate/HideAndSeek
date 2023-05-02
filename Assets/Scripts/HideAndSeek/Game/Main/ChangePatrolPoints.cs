namespace HideAndSeek
{
    public class ChangePatrolPoints
    {
        private readonly EnemySpawner _spawner;

        public PatrolQueue CurrectQueue { get; private set; }

        public ChangePatrolPoints(EnemySpawner spawner)
        {
            _spawner = spawner;
        }

        public void Clear()
        {
            CurrectQueue = PatrolQueue.First;
        }

        public void SetQueue(PatrolQueue queue)
        {
            CurrectQueue = queue;

            foreach (var enemy in _spawner.Enemys)
            {
                enemy.Patrol.SetNextQueue(queue);
            }
        }
    }
}
