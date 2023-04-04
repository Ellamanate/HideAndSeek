using Zenject;

namespace HideAndSeek.AI
{
    public class Search : BaseAction
    {
        private readonly EnemyMovement _movement;

        public Search(EnemyMovement movement)
        {
            _movement = movement;
        }

        public override void Execute()
        {
            _movement.StopChase();
        }

        public class Factory : PlaceholderFactory<Search> { }
    }
}
