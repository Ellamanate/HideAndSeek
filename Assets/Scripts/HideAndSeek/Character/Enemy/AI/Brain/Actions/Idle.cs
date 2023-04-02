using Zenject;

namespace HideAndSeek.AI
{
    public class Idle : BaseAction
    {
        private readonly EnemyMovement _movement;

        public Idle(EnemyMovement movement)
        {
            _movement = movement;
        }

        public override void Execute()
        {
            _movement.StopChase();
        }

        public class Factory : PlaceholderFactory<Idle> { }
    }
}
