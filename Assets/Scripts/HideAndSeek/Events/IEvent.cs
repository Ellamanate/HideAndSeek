namespace HideAndSeek
{
    public interface IEvent
    {
        public bool CanInvoke();
        public void Invoke();
    }
}
