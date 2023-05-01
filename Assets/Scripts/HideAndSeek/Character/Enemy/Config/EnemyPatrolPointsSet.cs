using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyPatrolPointsSet : SerializedMonoBehaviour
    {
        [SerializeField] private Dictionary<PatrolQueue, PatrolPoint[]> _patrolPointsSet;

        public bool TryGetPatrolPoints(PatrolQueue queue, out PatrolPoint[] points)
        {
            return _patrolPointsSet.TryGetValue(queue, out points);
        }
    }
}
