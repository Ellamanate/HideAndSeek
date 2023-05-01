using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyMovement : IDisposable, ITickable
    {
        private readonly EnemyModel _model;
        private readonly EnemyBody _body;
        private readonly EnemyPatrol _patrol;
        private readonly EnemyUpdateBrain _brain;
        private readonly EnemySightMovement _sight;
        private readonly GamePause _pause;

        private CancellationTokenSource _token;
        private ITransformable _target;

        private float StoppingDistance => _body.Movement.StoppingDistance;
        private bool Moved => _model.Moved && !_body.Movement.IsStopped && !_body.Movement.PathPending;
        private bool NeedStop => Moved && (HasPathAndCompleted || NoPathAndMove);
        private bool HasPathAndCompleted => _body.Movement.HasPath && PathCompleted;
        private bool NoPathAndMove => !_body.Movement.HasPath 
            && (PathCompleted || (_body.Movement.Velocity.sqrMagnitude <= 0.01f && _body.Movement.Acceleration <= 0.01f));
        private bool PathCompleted => _body.Movement.RemainingDistance <= StoppingDistance;

        public EnemyMovement(EnemyModel model, EnemyBody body, EnemyPatrol patrol, EnemyUpdateBrain brain, 
            EnemySightMovement sight, GamePause pause)
        {
            _model = model;
            _body = body;
            _patrol = patrol;
            _brain = brain;
            _sight = sight;
            _pause = pause;
            _token = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }
        
        public void Tick()
        {
            if (!_model.Destroyed && _model.Active && NeedStop && !_pause.Paused)
            {
                StopMovement();
            }
        }

        public void MoveTo(Vector3 destination)
        {
            if (!_model.Destroyed && _model.Active)
            {
                StopChase();
                SetDestination(destination);
            }
        }

        public void StopMovement()
        {
            if (!_model.Destroyed)
            {
                StopChase();

                _body.Movement.Stop();
                _model.Moved = false;
                GameLogger.Log("Enemy stopped");

                if (_model.Active)
                {
                    if (_patrol.StandsAtPatrolPoint())
                    {
                        _patrol.PlayPointAnimation();
                        _patrol.SetNextPoint();
                    }
                    else
                    {
                        _brain.UpdateAction();
                    }
                }
            }
        }
        
        public void ChaseTo(ITransformable target)
        {
            if (!_model.Destroyed)
            {
                _sight.SetLookAtTransformableTarget(_target);
                _patrol.CancelLookAround();
                _brain.CacelRelaxTransition();
                _target = target;

                _token = _token.Refresh();
                _ = UpdateChase(_token.Token);
            }
        }

        public void StopChase()
        {
            _sight.SetLookAtTransformableTarget(null);
            _token.TryCancel();
            _target = null;
        }

        private void SetDestination(Vector3 destination)
        {
            _patrol.CancelLookAround();
            _body.Movement.MoveTo(destination);
            _model.Destination = destination;
            _model.Moved = true;
        }

        private async UniTask UpdateChase(CancellationToken token)
        {
            while (!_model.Destroyed && _model.Active && !token.IsCancellationRequested && _target != null)
            {
                if (!_pause.Paused)
                {
                    SetDestination(_target.Position);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_model.RepathTime), cancellationToken: token);
            }
        }
    }
}
