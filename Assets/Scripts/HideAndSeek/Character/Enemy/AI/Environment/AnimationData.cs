using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace HideAndSeek
{
    [Serializable]
    public struct AnimationData
    {
        public Ease Ease;
        public bool SpeedBased;
        [ShowIf(nameof(SpeedBased))] public float Speed;
        [HideIf(nameof(SpeedBased))] public float Duration;
        public float Wait;
    }
}