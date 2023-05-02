using System;
using System.Threading;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class GameMenu : IDisposable
    {
        private readonly GamePause _pause;
        private readonly PauseMenu _pauseMenu;
        private readonly SetGameState _gameState;
        private readonly InputSystem _inputSystem;
        private readonly MainGameMediator _mediator;

        private CancellationTokenSource _token;

        public GameMenu(GamePause pause, SetGameState gameState, PauseMenu pauseMenu, 
            InputSystem inputSystem, MainGameMediator mediator)
        {
            _pause = pause;
            _pauseMenu = pauseMenu;
            _gameState = gameState;
            _inputSystem = inputSystem;
            _mediator = mediator;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void OpenPauseMenu()
        {
            if (!_gameState.GameOver)
            {
                _token = _token.Refresh();
                _pauseMenu.OpenPauseMenu(_token.Token);
                _pause.Pause();
            }
        }

        public void ClosePauseMenu()
        {
            if (!_gameState.GameOver)
            {
                _token = _token.Refresh();
                _pauseMenu.ClosePauseMenu(_token.Token);
                _pause.Unpause();
            }
        }
    }
}