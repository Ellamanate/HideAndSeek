namespace HideAndSeek
{
    public interface IInteractable<T> : ILimitingInteraction, IPositionedInteraction
    {
        public bool TouchTrigger { get; }
        public void Interact(T interactor);
    }
}
