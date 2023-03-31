using System;
using UnityEngine;

namespace HideAndSeek
{
    [Serializable]
    public struct EnemySpawnData
    {
        public EnemyConfig Config;
        public Transform Position;
    }
}
