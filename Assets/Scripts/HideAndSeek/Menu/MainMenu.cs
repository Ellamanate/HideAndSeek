using Infrastructure;

namespace HideAndSeek
{
    public class MainMenu
    {
        private readonly GameStateMachine _gameStateMachine;

        public MainMenu(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void PlayGame() => _gameStateMachine.MoveToState<MainGameState>();
    }
}
