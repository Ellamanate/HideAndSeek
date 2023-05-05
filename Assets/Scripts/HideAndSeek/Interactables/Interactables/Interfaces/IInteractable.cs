namespace HideAndSeek
{
    public interface IInteractable<T> : ILimitingInteraction
    {
        public bool TouchTrigger { get; }
        public void Interact(T interactor);
    }
}
