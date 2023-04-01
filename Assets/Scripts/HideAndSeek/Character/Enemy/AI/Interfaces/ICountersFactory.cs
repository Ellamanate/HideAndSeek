using System;
using System.Collections.Generic;

namespace HideAndSeek.AI
{
    public interface ICountersFactory<T> where T : Enum
    {
        public List<IScoreCounter> Create(T types);
    }
}
