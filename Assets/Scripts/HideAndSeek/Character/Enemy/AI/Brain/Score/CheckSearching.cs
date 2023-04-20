using Zenject;

namespace HideAndSeek.AI
{
    public class CheckSearching : IScoreCounter
    {
        private readonly Enemy _enemy;
        private readonly EnemyPatrol _patrol;
        private readonly Actions<OrderActionType> _actions;

        public CheckSearching(Actions<OrderActionType> actions, Enemy enemy, EnemyPatrol patrol)
        {
            _actions = actions;
            _enemy = enemy;
            _patrol = patrol;
        }

        public void CalculateScore()
        {
            if (_enemy.Model.Moved && SearchingState() && DestinationNotPatrol())
            {
                _actions.AddScoreTo(OrderActionType.Patrol, 0.5f);
            }

            bool SearchingState() => _enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching;
            bool DestinationNotPatrol() => !_patrol.IsPatrolPoint(_enemy.Model.Destination);
        }

        public class Factory : PlaceholderFactory<CheckSearching> { }
    }
}