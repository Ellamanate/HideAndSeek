using Infrastructure;

namespace HideAndSeek
{
    public class MainGame
    {
        private GameStateMachine _gameStateMachine;
        private StartGame _startGame;

        public MainGame(GameStateMachine gameStateMachine, StartGame startGame)
        {
            _gameStateMachine = gameStateMachine;
            _startGame = startGame;
        }

        public void Exit() => _gameStateMachine.MoveToState<MenuState>();

        public void StartGame()
        {
            _startGame.Start();
        }
    }
}
