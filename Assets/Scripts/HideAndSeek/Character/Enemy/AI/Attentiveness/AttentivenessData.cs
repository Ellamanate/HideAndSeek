using System;

namespace HideAndSeek.AI
{
    [Serializable]
    public struct AttentivenessData
    {
        public float Speed;
        public float VisionDistance;
        public float VisionAngle;
        public float TimeToDetect;
        public float WakeUpTimer;
        public ConeData ConeData;
    }
}