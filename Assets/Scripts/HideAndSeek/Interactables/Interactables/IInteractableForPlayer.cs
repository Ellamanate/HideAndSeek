namespace HideAndSeek
{
    public interface IInteractableForPlayer
    {
        public bool CanPlayerInteract { get; }
        public bool TouchTrigger { get; }
        public void Interact(Player player);
    }
}
