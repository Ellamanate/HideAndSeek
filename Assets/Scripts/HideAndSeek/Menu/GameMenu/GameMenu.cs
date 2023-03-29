using System;

namespace HideAndSeek
{
    public class GameMenu : IDisposable
    {
        private readonly MainGame _mainGame;
        private readonly InputSystem _inputSystem;
        private readonly MainGameMediator _mediator;

        public GameMenu(MainGame mainGame, InputSystem inputSystem, MainGameMediator mediator)
        {
            _mainGame = mainGame;
            _inputSystem = inputSystem;
            _mediator = mediator;
            _inputSystem.OnBack += Navigate;
        }

        public void Dispose()
        {
            _inputSystem.OnBack -= Navigate;
        }

        private void Navigate()
        {
            
        }
    }
}
