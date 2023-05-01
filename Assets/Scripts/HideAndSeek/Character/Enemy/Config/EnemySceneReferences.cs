using System;
using UnityEngine;

namespace HideAndSeek
{
    [Serializable]
    public class EnemySceneReferences
    {
        [field: SerializeField] public EnemyConfig Config { get; private set; }
        [field: SerializeField] public Transform SpawnPosition { get; private set; }
        [field: SerializeField] public PatrolPoint[] PatrolPoints { get; private set; }
        [field: SerializeField] public EnemyPatrolPointsSet PatrolPointsSet { get; private set; }
    }
}
