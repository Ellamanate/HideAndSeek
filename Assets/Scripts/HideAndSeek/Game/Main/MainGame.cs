using Infrastructure;

namespace HideAndSeek
{
    public class MainGame
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SetGameState _gameState;

        public MainGame(GameStateMachine gameStateMachine, SetGameState gameState)
        {
            _gameStateMachine = gameStateMachine;
            _gameState = gameState;
        }

        public void Exit() => _gameStateMachine.MoveToState<MenuState>();
        
        public void RestartGame()
        {
            _gameState.RestartGame();
        }

        public void CompleteGame()
        {
            _gameState.CompleteGame();
        }

        public void FailGame()
        {
            _gameState.FailGame();
        }
    }
}
