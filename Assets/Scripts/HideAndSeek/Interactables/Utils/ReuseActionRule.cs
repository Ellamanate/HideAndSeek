﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace HideAndSeek
{
    [System.Serializable]
    public struct ReuseActionRule
    {
        public InteractorType InteractorType;
        public bool Unlimit;
        [Min(1), HideIf(nameof(Unlimit))] public int Limit;
    }
}