using System;

namespace HideAndSeek
{
    public interface IDestroyable
    {
        public event Action OnDestroyed;
        public void Destroy();
    }
}
