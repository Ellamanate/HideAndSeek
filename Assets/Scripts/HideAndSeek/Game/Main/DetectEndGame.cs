using System;

namespace HideAndSeek
{
    public class DetectEndGame : IDisposable
    {
        private readonly MainGame _game;
        private readonly Player _player;

        public DetectEndGame(MainGame game, Player player)
        {
            _game = game;
            _player = player;

            _player.OnInteractableEnter += CheckFinish;
        }

        public void Dispose()
        {
            _player.OnInteractableEnter -= CheckFinish;
        }

        private void CheckFinish(IInteractable interactable)
        {
            if (interactable is ExitInteraction)
            {
                _game.CompleteGame();
            }
        }
    }
}
