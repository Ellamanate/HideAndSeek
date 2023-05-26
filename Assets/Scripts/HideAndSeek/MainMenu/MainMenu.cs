using Infrastructure;

namespace HideAndSeek
{
    public class MainMenu
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelsService _levelsService;

        public MainMenu(GameStateMachine gameStateMachine, LevelsService levelsService)
        {
            _gameStateMachine = gameStateMachine;
            _levelsService = levelsService;
        }

        public void PlayGame(LevelData levelData)
        {
            _levelsService.SelectLevel(levelData);
            _gameStateMachine.MoveToState<MainGameState>();
        }
    }
}
