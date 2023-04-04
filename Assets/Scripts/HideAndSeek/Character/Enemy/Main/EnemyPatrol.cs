using System;
using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyPatrol : IDisposable
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _movement;
        private readonly EnemySceneConfig _enemySceneConfig;

        private int _currentPatrolIndex;

        public EnemyPatrol(Enemy enemy, EnemyMovement movement, EnemySceneConfig enemySceneConfig)
        {
            _enemy = enemy;
            _movement = movement;
            _enemySceneConfig = enemySceneConfig;

            _enemy.OnInitialized += Reset;
            _enemy.OnStopped += ApplyPosition;
        }

        public void Dispose()
        {
            _enemy.OnInitialized -= Reset;
            _enemy.OnStopped -= ApplyPosition;
        }

        public void Patrol()
        {
            _movement.MoveTo(_enemySceneConfig.PatrolPositions[_currentPatrolIndex]);
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return _enemySceneConfig.PatrolPositions.Any(x => x == position);
        }

        private void ApplyPosition()
        {
            if (Vector3.Distance(_enemy.Model.Position, _enemySceneConfig.PatrolPositions[_currentPatrolIndex]) < _movement.StoppingDistance)
            {
                _currentPatrolIndex++;

                if (_currentPatrolIndex >= _enemySceneConfig.PatrolPositions.Length)
                {
                    _currentPatrolIndex = 0;
                }
            }
        }

        private void Reset()
        {
            _currentPatrolIndex = 0;
        }
    }
}
