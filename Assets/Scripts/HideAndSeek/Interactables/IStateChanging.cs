using System;

namespace HideAndSeek
{
    public interface IStateChanging
    {
        public event Action OnStateChanged;
    }
}