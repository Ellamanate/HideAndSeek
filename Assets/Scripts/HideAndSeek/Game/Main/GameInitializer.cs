namespace HideAndSeek
{
    public class GameInitializer
    {
        private readonly MainGame _game;
        private readonly PlayerFactory _playerFactory;
        private readonly EnemySpawner _enemySpawner;

        public GameInitializer(MainGame game, PlayerFactory playerFactory, EnemySpawner enemySpawner)
        {
            _game = game;
            _playerFactory = playerFactory;
            _enemySpawner = enemySpawner;
        }

        public void Initialize()
        {
            _playerFactory.Create();
            _enemySpawner.Spawn();
            _game.RestartGame();
        }
    }
}