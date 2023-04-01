using Zenject;

namespace HideAndSeek.AI
{
    public class Idle : BaseAction
    {
        private readonly Enemy _enemy;

        public Idle(Enemy enemy)
        {
            _enemy = enemy;
        }

        public override void Execute()
        {

        }

        public class Factory : PlaceholderFactory<Idle> { }
    }
}
