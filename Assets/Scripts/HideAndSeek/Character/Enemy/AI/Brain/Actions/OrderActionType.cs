using System;

namespace HideAndSeek.AI
{
    [Flags]
    public enum OrderActionType
    {
        None = 0,
        Idle = 1 << 0,
        Chase = 1 << 1,
        Search = 1 << 2,
        Patrol = 1 << 3
    }
}
