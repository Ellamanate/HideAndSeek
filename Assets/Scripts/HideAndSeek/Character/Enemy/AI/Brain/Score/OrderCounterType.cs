using System;

namespace HideAndSeek.AI
{
    [Flags]
    public enum OrderCounterType
    {
        None = 0,
        CheckVisible = 1 << 0,
        CheckSleep = 1 << 1,
        CheckSearching = 1 << 2,
        CheckInteract = 1 << 3,
    }
}
