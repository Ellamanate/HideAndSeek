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
            if (_enemy.Model.Moved && Searching() && NotPatrol())
            {
                _actions.AddScoreTo(OrderActionType.Search, 0.5f);
            }

            bool Searching() => _enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching;
            bool NotPatrol() => !_patrol.IsPatrolPoint(_enemy.Model.Destination);
        }

        public class Factory : PlaceholderFactory<CheckSearching> { }
    }
}