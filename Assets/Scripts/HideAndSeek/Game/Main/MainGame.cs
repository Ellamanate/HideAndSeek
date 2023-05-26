using Infrastructure;

namespace HideAndSeek
{
    public class MainGame
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SetGameState _gameState;
        private readonly LevelsService _levelsService;

        public MainGame(GameStateMachine gameStateMachine, SetGameState gameState, LevelsService levelsService)
        {
            _gameStateMachine = gameStateMachine;
            _gameState = gameState;
            _levelsService = levelsService;
        }

        public void Exit() => _gameStateMachine.MoveToState<MenuState>();
        public void LoadLevel() => _gameStateMachine.MoveToState<MainGameState>();
        
        public void StartGame()
        {
            _gameState.StartGame();
        }

        public void CompleteGame()
        {
            _levelsService.CompleteLevel();
            _gameState.CompleteGame();
        }
    }
}
