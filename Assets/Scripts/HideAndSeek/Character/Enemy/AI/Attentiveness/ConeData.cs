using System;
using UnityEngine;

namespace HideAndSeek.AI
{
    [Serializable]
    public struct ConeData
    {
        public Color VisionColor;
        public Color NonVisionColor;
        public float Distance;
        public float Angle;
    }
}