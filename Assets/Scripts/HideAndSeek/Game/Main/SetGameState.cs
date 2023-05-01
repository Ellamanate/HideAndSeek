using System;
using System.Threading;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class SetGameState : IDisposable
    {
        private readonly StartGame _startGame;
        private readonly GameOver _gameOver;

        private CancellationTokenSource _token;

        public bool GameOver { get; private set; }

        public SetGameState(StartGame startGame, GameOver endGame)
        {
            _startGame = startGame;
            _gameOver = endGame;
        }

        public void Dispose() => _token.CancelAndDispose();

        public void StartGame()
        {
            GameLogger.Log("Start game");
            GameOver = false;
            _token = _token.Refresh();
            _ = _startGame.Start(_token.Token);
        }

        public void CompleteGame()
        {
            GameLogger.Log("Complete game");
            GameOver = true;
            _token = _token.Refresh();
            _ = _gameOver.CompleteGame(_token.Token);
        }

        public void FailGame()
        {
            GameLogger.Log("Fail game");
            GameOver = true;
            _token = _token.Refresh();
            _ = _gameOver.FailGame(_token.Token);
        }
    }
}