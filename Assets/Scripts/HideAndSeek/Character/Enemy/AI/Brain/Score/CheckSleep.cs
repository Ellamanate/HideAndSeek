using Zenject;

namespace HideAndSeek.AI
{
    public class CheckSleep : IScoreCounter
    {
        private readonly EnemyWakeUp _wakeUp;
        private readonly Actions<OrderActionType> _actions;

        public CheckSleep(Actions<OrderActionType> actions, EnemyWakeUp wakeUp)
        {
            _actions = actions;
            _wakeUp = wakeUp;
        }

        public void CalculateScore()
        {
            if (_wakeUp.EnemySleepd)
            {
                _actions.AddScoreTo(OrderActionType.Patrol, 1);
            }
        }

        public class Factory : PlaceholderFactory<CheckSleep> { }
    }
}