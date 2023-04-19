using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyPatrol : IDisposable
    {
        private readonly Enemy _enemy;
        private readonly EnemyMovement _movement;
        private readonly EnemySceneConfig _enemySceneConfig;

        private CancellationTokenSource _token;
        private int _currentPatrolIndex;

        public bool PlayingLookAround { get; private set; }

        public EnemyPatrol(Enemy enemy, EnemyMovement movement, EnemySceneConfig enemySceneConfig)
        {
            _enemy = enemy;
            _movement = movement;
            _enemySceneConfig = enemySceneConfig;

            _enemy.OnInitialized += Reset;
            _enemy.OnStopped += ApplyPosition;
            _enemy.OnDestinationChanged += CancelLookAround;
            _enemy.OnDestroyed += CancelLookAround;
        }

        public void Dispose()
        {
            _enemy.OnInitialized -= Reset;
            _enemy.OnStopped -= ApplyPosition;
            _enemy.OnDestinationChanged -= CancelLookAround;
            _enemy.OnDestroyed -= CancelLookAround;
            _token.CancelAndDispose();
        }

        public void Patrol()
        {
            var currentPatrolPoint = _enemySceneConfig.PatrolPositions[_currentPatrolIndex];
            _movement.MoveTo(currentPatrolPoint.transform.position);
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return _enemySceneConfig.PatrolPositions.Any(x => x.transform.position == position);
        }

        private void ApplyPosition()
        {
            var currentPatrolPoint = _enemySceneConfig.PatrolPositions[_currentPatrolIndex];

            if (Vector3.Distance(_enemy.Model.Position, currentPatrolPoint.transform.position) < _movement.StoppingDistance)
            {
                _currentPatrolIndex++;

                _token = _token.Refresh();
                _ = LookAround(currentPatrolPoint, _token.Token);

                if (_currentPatrolIndex >= _enemySceneConfig.PatrolPositions.Length)
                {
                    _currentPatrolIndex = 0;
                }
            }
        }

        private void CancelLookAround()
        {
            if (PlayingLookAround)
            {
                _token.TryCancel();
            }
        }

        private void Reset()
        {
            _currentPatrolIndex = 0;
            PlayingLookAround = false;
            _token.TryCancel();
        }

        private async UniTask LookAround(PatrolPoint point, CancellationToken token)
        {
            try
            {
                PlayingLookAround = true;

                await point.PlayAnimation(_enemy, token);

                PlayingLookAround = false;
                _enemy.UpdateAction();
            }
            finally
            {
                PlayingLookAround = false;
            }
        }
    }
}
