using Zenject;

namespace HideAndSeek.AI
{
    public class CheckInteraction : IScoreCounter
    {
        private readonly Enemy _enemy;
        private readonly HidePlayer _hidePlayer;
        private readonly Actions<OrderActionType> _actions;

        public CheckInteraction(Actions<OrderActionType> actions, Enemy enemy, HidePlayer hidePlayer)
        {
            _actions = actions;
            _enemy = enemy;
            _hidePlayer = hidePlayer;
        }

        public void CalculateScore()
        {
            if (_enemy.Model.PlayerDetectedInShelter && _enemy.Interact.Contains(_hidePlayer.CurrentShelter))
            {
                _actions.SetOnly(OrderActionType.Interact, 1);
            }
            else if (_enemy.Interact.CanInteract())
            {
                _actions.AddScoreTo(OrderActionType.Interact, 0.5f);
            }
        }

        public class Factory : PlaceholderFactory<CheckInteraction> { }
    }
}