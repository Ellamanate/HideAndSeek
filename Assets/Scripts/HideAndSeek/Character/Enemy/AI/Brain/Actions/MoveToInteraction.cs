using Zenject;

namespace HideAndSeek.AI
{
    public class MoveToInteraction : BaseAction
    {
        private readonly Enemy _enemy;
        private readonly SceneInteractions _sceneInteractions;

        public MoveToInteraction(Enemy enemy, SceneInteractions sceneInteractions)
        {
            _enemy = enemy;
            _sceneInteractions = sceneInteractions;
        }

        public override void Execute()
        {
            if (_sceneInteractions.TryGetPriorityInteractionNear(_enemy, out var interactable))
            {
                _enemy.Movement.MoveTo((interactable as IPositionedInteraction).InteractionPosition);
            }
        }

        public class Factory : PlaceholderFactory<MoveToInteraction> { }
    }
}
