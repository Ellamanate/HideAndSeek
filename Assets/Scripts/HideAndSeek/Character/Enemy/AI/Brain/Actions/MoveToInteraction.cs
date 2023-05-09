using Zenject;

namespace HideAndSeek.AI
{
    public class MoveToInteraction : BaseAction
    {
        private readonly Enemy _enemy;
        private readonly EnemyModel _model;
        private readonly SearchingPatrol _searchingPatrol;
        private readonly EnemyPatrol _patrol;
        private readonly SceneInteractions _sceneInteractions;
        private readonly HidePlayer _hidePlayer;

        public MoveToInteraction(Enemy enemy, EnemyModel model, EnemyPatrol patrol, SearchingPatrol searchingPatrol,
            SceneInteractions sceneInteractions, HidePlayer hidePlayer)
        {
            _enemy = enemy;
            _model = model;
            _searchingPatrol = searchingPatrol;
            _patrol = patrol;
            _sceneInteractions = sceneInteractions;
            _hidePlayer = hidePlayer;
        }

        public override void Execute()
        {
            if (_model.PlayerDetectedInShelter)
            {
                _enemy.Movement.MoveTo(_hidePlayer.CurrentShelter.InteractionPosition);
                return;
            }
            else
            {
                if (_patrol.SearchingState && _searchingPatrol.CurrentPoint != null) 
                {
                    var shelter = _searchingPatrol.CurrentPoint.GetRandomShelter();

                    if (shelter != null)
                    {
                        _enemy.Movement.MoveTo(shelter.InteractionPosition);
                        return;
                    }
                }

                if (_sceneInteractions.TryGetInteractionNear(_enemy, out var interactable))
                {
                    _enemy.Movement.MoveTo(interactable.InteractionPosition);
                    return;
                }
            }

            GameLogger.LogError("No interactions");
        }

        public class Factory : PlaceholderFactory<MoveToInteraction> { }
    }
}
