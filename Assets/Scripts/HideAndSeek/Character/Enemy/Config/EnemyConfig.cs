using System;
using HideAndSeek.AI;
using UnityEngine;

namespace HideAndSeek
{
    [CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyBody EnemyPrefab { get; private set; }
        [field: SerializeField] public AttentivenessEnemyConfig RelaxState { get; private set; }
        [field: SerializeField] public AttentivenessEnemyConfig SearchingState { get; private set; }
        [field: SerializeField] public AttentivenessEnemyConfig ChaseState { get; private set; }
        [field: SerializeField] public float RepathTime { get; private set; }
        [field: SerializeField] public LayerMask RaycastLayers { get; private set; }
        [field: SerializeField] public OrderActionType Actions { get; private set; }
        [field: SerializeField] public OrderCounterType Counters { get; private set; }
    }
}
