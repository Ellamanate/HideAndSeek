using System;
using System.Collections.Generic;

namespace HideAndSeek.AI
{
    public interface IActionsFactory<T> where T : Enum
    {
        public Dictionary<T, IAction> Create(T types);
    }
}
