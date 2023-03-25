using Zenject;

namespace Infrastructure
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly BootstrapConfig _bootstrapConfig;

        public BootstrapState(GameStateMachine stateMachine, BootstrapConfig bootstrapConfig)
        {
            _stateMachine = stateMachine;
            _bootstrapConfig = bootstrapConfig;
        }

        public void Enter()
        {
            if (_bootstrapConfig.StartFromMenu)
            {
                _stateMachine.MoveToState<MenuState>();
            }
            else
            {
                _stateMachine.MoveToState<MainGameState>();
            }
        }

        public class Factory : PlaceholderFactory<GameStateMachine, BootstrapState> { }
    }
}
