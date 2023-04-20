namespace HideAndSeek
{
    public class EnemyInteract
    {
        private readonly MainGame _mainGame;

        public EnemyInteract(MainGame mainGame)
        {
            _mainGame = mainGame;
        }

        public void Interact(IInteractable interactable)
        {
            if (interactable is PlayerBody)
            {
                _mainGame.FailGame();
            }
        }
    }
}
