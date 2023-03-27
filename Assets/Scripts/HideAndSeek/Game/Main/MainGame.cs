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
        private readonly StartGame _startGame;
        private readonly GameOver _gameOver;

        private CancellationTokenSource _token;

        public MainGame(GameStateMachine gameStateMachine, PlayerSpawner playerSpawner, StartGame startGame, GameOver endGame)
        {
            _gameStateMachine = gameStateMachine;
            _playerSpawner = playerSpawner;
            _startGame = startGame;
            _gameOver = endGame;
            _token = new CancellationTokenSource();
        }

        public void Dispose() => _token.CancelAndDispose();
        public void Exit() => _gameStateMachine.MoveToState<MenuState>();

        public void Initialize()
        {
            _playerSpawner.Spawn();

            _token = _token.Refresh();
            _ = _startGame.Start(_token.Token);
        }

        public void CompleteGame()
        {
            _token = _token.Refresh();
            _ = _gameOver.CompleteGame(_token.Token);
        }

        public void GameOver()
        {
            _token = _token.Refresh();
            _ = _gameOver.FailGame(_token.Token);
        }
    }
}
