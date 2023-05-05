using UnityEngine;

namespace HideAndSeek
{
    [System.Serializable]
    public struct TimeReuseRule
    {
        public InteractorType InteractorType;
        [Min(0.01f)] public float TimeToReuse;
    }
}