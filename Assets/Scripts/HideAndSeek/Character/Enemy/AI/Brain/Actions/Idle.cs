using Zenject;

namespace HideAndSeek.AI
{
    public class Idle : BaseAction
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _movement;

        public Idle(Enemy enemy, EnemyMovement movement)
        {
            _enemy = enemy;
            _movement = movement;
        }

        public override void Execute()
        {
            _movement.StopChase();
            _enemy.StopMovement();
        }

        public class Factory : PlaceholderFactory<Idle> { }
    }
}
