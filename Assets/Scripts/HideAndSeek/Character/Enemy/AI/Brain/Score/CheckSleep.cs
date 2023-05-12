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
            if (!_enemy.Model.Moved && _enemy.Model.CurrentAttentiveness != AttentivenessType.Chase && !_patrol.PlayingLookAround)
            {
                if (_enemy.Model.CurrentAttentiveness == AttentivenessType.Relax)
                {
                    _actions.AddScoreTo(OrderActionType.Patrol, 0.1f);
                }
                else if (_enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching)
                {
                    _actions.AddScoreTo(OrderActionType.Search, 0.1f);
                }
            }
        }

        public class Factory : PlaceholderFactory<CheckSleep> { }
    }
}