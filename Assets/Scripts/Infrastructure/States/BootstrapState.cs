using HideAndSeek;
using Zenject;

namespace Infrastructure
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly BootstrapConfig _bootstrapConfig;
        private readonly LevelsService _levels;

        public BootstrapState(GameStateMachine stateMachine, BootstrapConfig bootstrapConfig, LevelsService levels)
        {
            _stateMachine = stateMachine;
            _bootstrapConfig = bootstrapConfig;
            _levels = levels;
        }

        public void Enter()
        {
            if (_bootstrapConfig.StartFromMenu)
            {
                _stateMachine.MoveToState<MenuState>();
            }
            else
            {
                _levels.SelectLevel(_bootstrapConfig.LoadLevel);
                _stateMachine.MoveToState<MainGameState>();
            }
        }

        public class Factory : PlaceholderFactory<GameStateMachine, BootstrapState> { }
    }
}
