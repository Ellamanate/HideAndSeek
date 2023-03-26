using Infrastructure;

namespace HideAndSeek
{
    public class MainGame
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PlayerSpawner _playerSpawner;
        private readonly HideAdsSeekGame.Factory _gameFactory;

        private HideAdsSeekGame _gameInstance;

        public MainGame(GameStateMachine gameStateMachine, PlayerSpawner playerSpawner, HideAdsSeekGame.Factory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _playerSpawner = playerSpawner;
            _gameFactory = gameFactory;
        }

        public void Exit() => _gameStateMachine.MoveToState<MenuState>();

        public void StartGame()
        {
            Player player = _playerSpawner.Spawn();
            _gameInstance = _gameFactory.Create(player);
            _gameInstance.Start();
        }
    }
}
