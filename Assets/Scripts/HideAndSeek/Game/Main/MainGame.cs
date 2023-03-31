using System;
using System.Threading;
using HideAndSeek.Extensions;
using Infrastructure;

namespace HideAndSeek
{
    public class MainGame : IDisposable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PlayerSpawner _playerSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly StartGame _startGame;
        private readonly GameOver _gameOver;
        private readonly GamePause _pause;
        private readonly PauseMenu _pauseMenu;

        private CancellationTokenSource _token;

        public bool GameOver { get; private set; }

        public MainGame(GameStateMachine gameStateMachine, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            StartGame startGame, GameOver endGame, GamePause pause, PauseMenu pauseMenu)
        {
            _gameStateMachine = gameStateMachine;
            _playerSpawner = playerSpawner;
            _enemySpawner = enemySpawner;
            _startGame = startGame;
            _gameOver = endGame;
            _pause = pause;
            _pauseMenu = pauseMenu;
            _token = new CancellationTokenSource();
        }

        public void Dispose() => _token.CancelAndDispose();
        public void Exit() => _gameStateMachine.MoveToState<MenuState>();

        public void Initialize()
        {
            _playerSpawner.Spawn();
            _enemySpawner.Spawn();
            RestartGame();
        }

        public void RestartGame()
        {
            GameOver = false;
            _token = _token.Refresh();
            _ = _startGame.Start(_token.Token);
        }

        public void CompleteGame()
        {
            GameOver = true;
            _token = _token.Refresh();
            _ = _gameOver.CompleteGame(_token.Token);
        }

        public void FailGame()
        {
            GameOver = true;
            _token = _token.Refresh();
            _ = _gameOver.FailGame(_token.Token);
        }

        public void OpenPauseMenu()
        {
            if (!GameOver)
            {
                _token = _token.Refresh();
                _pauseMenu.OpenPauseMenu(_token.Token);
                _pause.Pause();
            }
        }

        public void ClosePauseMenu()
        {
            if (!GameOver)
            {
                _token = _token.Refresh();
                _pauseMenu.ClosePauseMenu(_token.Token);
                _pause.Unpause();
            }
        }
    }
}
