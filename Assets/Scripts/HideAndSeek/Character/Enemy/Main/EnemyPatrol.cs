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
        private readonly EnemyPatrolPointsSet _patrolSet;

        private PatrolPoint[] _patrolPoints;
        private CancellationTokenSource _token;
        private int _currentPatrolIndex;

        public bool PlayingLookAround { get; private set; }

        public EnemyPatrol(EnemyModel model, EnemyBody body, EnemyUpdateBrain brain,
            EnemyPatrolPointsSet patrolSet)
        {
            _model = model;
            _body = body;
            _brain = brain;
            _patrolSet = patrolSet;
            _patrolSet.TryGetPatrolPoints(PatrolQueue.First, out _patrolPoints);
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Reinitialize()
        {
            _currentPatrolIndex = 0;
            PlayingLookAround = false;
            _token.TryCancel();
            SetNextQueue(PatrolQueue.First);
        }

        public Vector3 GetCurrentPatrolPosition()
        {
            var currentPatrolPoint = _patrolPoints[_currentPatrolIndex];
            return currentPatrolPoint.transform.position;
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return _patrolPoints.Any(x => x.transform.position == position);
        }

        public bool StandsAtPatrolPoint()
        {
            var currentPatrolPoint = _patrolPoints[_currentPatrolIndex];
            return Vector3.Distance(_model.Position, currentPatrolPoint.transform.position) < _body.Movement.StoppingDistance * 2;
        }

        public void PlayPointAnimation()
        {
            _token = _token.Refresh();
            _ = LookAround(_patrolPoints[_currentPatrolIndex], _token.Token);
        }

        public void SetNextPoint()
        {
            _currentPatrolIndex++;

            if (_currentPatrolIndex >= _patrolPoints.Length)
            {
                _currentPatrolIndex = 0;
            }
        }

        public void SetNextQueue(PatrolQueue queue)
        {
            _patrolSet.TryGetPatrolPoints(queue, out _patrolPoints);
            _currentPatrolIndex = 0;
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
