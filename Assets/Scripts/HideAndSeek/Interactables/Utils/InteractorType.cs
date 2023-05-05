namespace HideAndSeek
{
    [System.Flags]
    public enum InteractorType
    {
        None = 0,
        Player = 1 << 0,
        Enemy = 1 << 1,
    }
}