using Zenject;

namespace HideAndSeek.AI
{
    public class CheckVisible : IScoreCounter
    {
        private readonly Enemy _enemy;
        private readonly Actions<OrderActionType> _actions;

        public CheckVisible(Actions<OrderActionType> actions, Enemy enemy)
        {
            _actions = actions;
            _enemy = enemy;
        }

        public void CalculateScore()
        {
            if (_enemy.Vision.PlayerVisible)
            {
                _actions.SetOnly(OrderActionType.Chase, 1);
            }
            else
            {
                _actions.Disable(OrderActionType.Chase);
            }
        }

        public class Factory : PlaceholderFactory<CheckVisible> { }
    }
}
