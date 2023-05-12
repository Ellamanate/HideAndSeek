using UnityEngine;

namespace HideAndSeek
{
    [System.Serializable]
    public struct ReuseTimeRule
    {
        public InteractorType InteractorType;
        [Min(0.01f)] public float TimeToReuse;
    }
}