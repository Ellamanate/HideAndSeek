namespace HideAndSeek
{
    public class PlayerInteract
    {
        private readonly MainGame _game;

        public PlayerInteract(MainGame game)
        {
            _game = game;
        }
        
        public void Interact(IInteractable interactable)
        {
            if (interactable is ExitInteraction)
            {
                _game.CompleteGame();
            }
            else if (interactable is Shelter shelter)
            {

            }
        }
    }
}
