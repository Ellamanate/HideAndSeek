using System;

namespace HideAndSeek
{
    public class EnemyTouch : IDisposable
    {
        private readonly Enemy _enemy;
        private readonly MainGame _mainGame;

        public EnemyTouch(Enemy enemy, MainGame mainGame)
        {
            _enemy = enemy;
            _mainGame = mainGame;

            _enemy.OnInteractableEnter += InteractableEnter;
        }

        public void Dispose()
        {
            _enemy.OnInteractableEnter -= InteractableEnter;
        }

        private void InteractableEnter(IInteractable interactable)
        {
            if (interactable is PlayerBody)
            {
                _mainGame.FailGame();
            }
        }
    }
}
