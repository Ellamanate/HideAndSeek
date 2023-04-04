using Zenject;

namespace HideAndSeek.AI
{
    public class Patrol : BaseAction
    {
        private readonly EnemyPatrol _patrol;

        public Patrol(EnemyPatrol patrol)
        {
            _patrol = patrol;
        }

        public override void Execute()
        {
            _patrol.Patrol();
        }

        public class Factory : PlaceholderFactory<Patrol> { }
    }
}
