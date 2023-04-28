namespace HideAndSeek
{
    public interface IInteractable
    {
        public bool TouchTrigger { get; }
        public void Interact();
    }
}
