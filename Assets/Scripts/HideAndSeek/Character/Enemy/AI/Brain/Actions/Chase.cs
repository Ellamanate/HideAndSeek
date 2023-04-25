using Zenject;

namespace HideAndSeek.AI
{
    public class Chase : BaseAction
    {
        private readonly EnemyMovement _movement;
        private readonly Player _player;

        public Chase(EnemyMovement movement, Player player)
        {
            _movement = movement;
            _player = player;
        }

        public override void Execute()
        {
            if (_player.Available)
            {
                _movement.ChaseTo(_player.UpdateBody);
            }
        }

        public class Factory : PlaceholderFactory<Chase> { }
    }
}
