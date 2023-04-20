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
        private readonly EnemyModel _model;
        private readonly EnemyBody _body;
        private readonly EnemyUpdateBrain _brain;
        private readonly EnemySceneConfig _enemySceneConfig;

        private CancellationTokenSource _token;
        private int _currentPatrolIndex;

        public bool PlayingLookAround { get; private set; }

        public EnemyPatrol(EnemyModel model, EnemyBody body, EnemyUpdateBrain brain,
            EnemySceneConfig enemySceneConfig)
        {
            _model = model;
            _body = body;
            _brain = brain;
            _enemySceneConfig = enemySceneConfig;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Initialize()
        {
            _currentPatrolIndex = 0;
            PlayingLookAround = false;
            _token.TryCancel();
        }

        public Vector3 GetCurrentPatrolPosition()
        {
            var currentPatrolPoint = _enemySceneConfig.PatrolPositions[_currentPatrolIndex];
            return currentPatrolPoint.transform.position;
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return _enemySceneConfig.PatrolPositions.Any(x => x.transform.position == position);
        }

        public bool TryApplyPosition()
        {
            var currentPatrolPoint = _enemySceneConfig.PatrolPositions[_currentPatrolIndex];

            if (Vector3.Distance(_model.Position, currentPatrolPoint.transform.position) < _body.Movement.StoppingDistance)
            {
                _currentPatrolIndex++;

                _token = _token.Refresh();
                _ = LookAround(currentPatrolPoint, _token.Token);

                if (_currentPatrolIndex >= _enemySceneConfig.PatrolPositions.Length)
                {
                    _currentPatrolIndex = 0;
                }

                return true;
            }

            return false;
        }

        public void CancelLookAround()
        {
            if (PlayingLookAround)
            {
                _token.TryCancel();
            }
        }

        private async UniTask LookAround(PatrolPoint point, CancellationToken token)
        {
            try
            {
                PlayingLookAround = true;

                await point.PlayAnimation(_model.Id, token);

                PlayingLookAround = false;
                _brain.UpdateAction();
            }
            finally
            {
                PlayingLookAround = false;
            }
        }
    }
}
