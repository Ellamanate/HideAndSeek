using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyPatrol : IDisposable
    {
        private readonly EnemyModel _model;
        private readonly EnemyUpdateBrain _brain;
        private readonly DefaultPatrol _defaultPatrol;
        private readonly SearchingPatrol _searchingPatrol;

        private BasePatrol _currentPatrol;
        private CancellationTokenSource _token;

        public bool PlayingLookAround { get; private set; }

        public bool DefaultState => _currentPatrol == _defaultPatrol;
        public bool SearchingState => _currentPatrol == _searchingPatrol;

        public EnemyPatrol(EnemyModel model, EnemyUpdateBrain brain, DefaultPatrol defaultPattrol, 
            SearchingPatrol searchingPatrol)
        {
            _model = model;
            _brain = brain;
            _defaultPatrol = defaultPattrol;
            _searchingPatrol = searchingPatrol;
            _currentPatrol = defaultPattrol;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Reinitialize()
        {
            _token.TryCancel();
            _searchingPatrol.DropPatrolPoints();
            _defaultPatrol.DropPatrolPoints();
            _defaultPatrol.SetNextQueue(PatrolQueue.First);
            PlayingLookAround = false;
        }

        public Vector3 GetCurrentPatrolPosition()
        {
            return _currentPatrol.GetCurrentPatrolPoint().transform.position;
        }

        public bool IsPatrolPoint(Vector3 position)
        {
            return _currentPatrol.IsPatrolPoint(position);
        }

        public void SetDefaultState()
        {
            _currentPatrol = _defaultPatrol;
        }

        public void SetSearchingState()
        {
            _searchingPatrol.DropPatrolPoints();
            _searchingPatrol.SetNearestSearchingPoint();
            _currentPatrol = _searchingPatrol;
        }

        public void MoveToNextPoint()
        {
            if (_currentPatrol.StandsAtPatrolPoint())
            {
                var currentPoint = _currentPatrol.GetCurrentPatrolPoint();
                SetNextPatrolPoint(currentPoint);
            }
            else
            {
                _brain.UpdateAction();
            }
        }

        public void SetNextQueue(PatrolQueue queue)
        {
            _defaultPatrol.SetNextQueue(queue);
        }

        public void CancelLookAround()
        {
            if (PlayingLookAround)
            {
                _token.TryCancel();
            }
        }

        private void SetNextPatrolPoint(PatrolPoint currentPoint)
        {
            _token = _token.Refresh();
            _ = LookAround(currentPoint, _token.Token);

            _currentPatrol.SetNextPoint();
        }

        private async UniTask LookAround(PatrolPoint patrolPoint, CancellationToken token)
        {
            PlayingLookAround = true;

            try
            {
                await patrolPoint.PlayAnimation(_model.Id, token);

                PlayingLookAround = false;
                _brain.UpdateAction();
            }
            catch
            {
                PlayingLookAround = false;
            }
        }
    }
}
