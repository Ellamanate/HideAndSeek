using System;

namespace HideAndSeek.AI
{
    [Flags]
    public enum OrderCounterType
    {
        None = 0,
        CheckVisible = 1 << 0
    }
}
