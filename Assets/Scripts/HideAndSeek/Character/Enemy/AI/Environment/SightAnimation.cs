using System;
using DG.Tweening;
using UnityEngine;

namespace HideAndSeek
{
    [Serializable]
    public struct SightAnimation
    {
        public Transform LookAt;
        public AnimationData Animation;

        public Ease Ease => Animation.Ease;
        public bool SpeedBased => Animation.SpeedBased;
        public float Speed => Animation.Speed;
        public float Duration => Animation.Duration;
        public float Wait => Animation.Wait;
    }
}