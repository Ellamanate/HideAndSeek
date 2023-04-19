using System;

namespace HideAndSeek.AI
{
    [Serializable]
    public struct AttentivenessData
    {
        public float Speed;
        public float VisionAngle;
        public float VisionDistance;
        public float TimeToDetect;
        public ConeData ConeData;
    }
}