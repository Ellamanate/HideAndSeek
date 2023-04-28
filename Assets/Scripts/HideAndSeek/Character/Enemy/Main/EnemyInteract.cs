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

        }

        public void TouchPlayer(PlayerBody body)
        {
            _mainGame.FailGame();
        }
    }
}
