using Zenject;

namespace HideAndSeek.AI
{
    public class CheckSleep : IScoreCounter
    {
        private readonly Actions<OrderActionType> _actions;
        private readonly Enemy _enemy;
        private readonly EnemyPatrol _patrol;

        public CheckSleep(Actions<OrderActionType> actions, Enemy enemy, EnemyPatrol patrol)
        {
            _actions = actions;
            _enemy = enemy;
            _patrol = patrol;
        }

        public void CalculateScore()
        {
            if (!_enemy.Model.Moved && _enemy.Model.CurrentAttentiveness == AttentivenessType.Relax && !_patrol.PlayingLookAround)
            {
                _actions.AddScoreTo(OrderActionType.Patrol, 1);
            }
        }

        public class Factory : PlaceholderFactory<CheckSleep> { }
    }
}