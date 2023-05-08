namespace HideAndSeek
{
    public class PlayerInteract : BaseInteract<Player>
    {
        private readonly PlayerHUD _hud;

        public PlayerInteract(PlayerHUD hud)
        {
            _hud = hud;
        }

        protected override InteractorType CurrentInteractorType => InteractorType.Player;

        public override bool CheckInteractionAvailable(IInteractable<Player> interactable)
        {
            return interactable.LimitInteract.CanPlayerInteract;
        }

        public override void Interact(Player player)
        {
            if (Interactables.Count > 0)
            {
                InteractAndLock(player, GetValidInteraction());
            }
        }

        protected override void OnInteractableAdded()
        {
            _hud.ShowInteractionIcon();
        }

        protected override void OnInteractableRemoved()
        {
            if (Interactables.Count == 0)
            {
                _hud.HideInteractionIcon();
            }
        }
    }
}
