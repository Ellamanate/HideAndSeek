using Zenject;

namespace HideAndSeek.AI
{
    public class CheckSearching : IScoreCounter
    {
        private readonly Enemy _enemy;
        private readonly SceneInteractions _sceneInteractions;
        private readonly Actions<OrderActionType> _actions;

        public CheckSearching(Actions<OrderActionType> actions, Enemy enemy, EnemyPatrol patrol,
            SceneInteractions sceneInteractions)
        {
            _actions = actions;
            _enemy = enemy;
            _sceneInteractions = sceneInteractions;
        }

        public void CalculateScore()
        {
            bool searchingState = SearchingState();

            if (_enemy.Model.Moved && searchingState)
            {
                _actions.AddScoreTo(OrderActionType.Search, 0.75f);
            }

            if (_enemy.Model.PlayerDetectedInShelter)
            {
                _actions.AddScoreTo(OrderActionType.MoveToInteraction, 1);
            }
            else if (searchingState && _sceneInteractions.TryGetInteractionNear(_enemy, out var interactable))
            {
                _actions.AddScoreTo(OrderActionType.MoveToInteraction, 0.5f);
            }

            bool SearchingState() => _enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching;
        }

        public class Factory : PlaceholderFactory<CheckSearching> { }
    }
}