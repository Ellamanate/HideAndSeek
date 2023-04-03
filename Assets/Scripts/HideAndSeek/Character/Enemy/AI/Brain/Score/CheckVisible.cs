using Zenject;

namespace HideAndSeek.AI
{
    public class CheckVisible : IScoreCounter
    {
        private readonly EnemyVision _vision;
        private readonly Actions<OrderActionType> _actions;

        public CheckVisible(Actions<OrderActionType> actions, EnemyVision vision)
        {
            _actions = actions;
            _vision = vision;
        }

        public void CalculateScore()
        {
            if (_vision.PlayerVisible)
            {
                _actions.AddScoreTo(OrderActionType.Chase, 1);
            }
            else
            {
                _actions.Disable(OrderActionType.Chase);
                _actions.AddScoreTo(OrderActionType.Patrol, 1);
            }
        }

        public class Factory : PlaceholderFactory<CheckVisible> { }
    }
}
