namespace HideAndSeek
{
    public class RestartGame
    {
        private readonly SetGameState _gameState;
        private readonly ChangePatrolPoints _changePatrolPoints;
        private readonly EnemySpawner _enemySpawner;
        private readonly PlayerInteract _interact;
        private readonly HidePlayer _hidePlayer;
        private readonly IResettable[] _resettables;

        public RestartGame(SetGameState gameState, ChangePatrolPoints changePatrolPoints, EnemySpawner enemySpawner,
            PlayerInteract interact, HidePlayer hidePlayer, IResettable[] resettables)
        {
            _gameState = gameState;
            _changePatrolPoints = changePatrolPoints;
            _enemySpawner = enemySpawner;
            _interact = interact;
            _hidePlayer = hidePlayer;
            _resettables = resettables;
        }

        public void Restart()
        {
            foreach (var resettable in _resettables)
            {
                resettable.ToDefault();
            }

            _changePatrolPoints.Clear();
            _enemySpawner.ResetEnemys();
            _interact.Clear();
            _hidePlayer.Show();
            _gameState.StartGame();
        }
    }
}
