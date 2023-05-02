using Zenject;

namespace HideAndSeek.AI
{
    public class Interact : BaseAction
    {
        private readonly Enemy _enemy;

        public Interact(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override void Execute()
        {
            _enemy.Interact.Interact(_enemy);
        }

        public class Factory : PlaceholderFactory<Interact> { }
    }
}
