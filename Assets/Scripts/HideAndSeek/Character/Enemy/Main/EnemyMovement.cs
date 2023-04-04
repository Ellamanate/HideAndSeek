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
        private readonly Enemy _enemy;
        private readonly EnemyBody _body;
        private readonly GamePause _pause;

        private CancellationTokenSource _token;
        private ITransformable _target;

        public float StoppingDistance => _body.Movement.StoppingDistance;
        public bool Moved => _enemy.Model.Moved && !_body.Movement.IsStopped && !_body.Movement.PathPending;

        private bool NeedStop => Moved && (HasPathAndCompleted || NoPathAndMove);
        private bool HasPathAndCompleted => _body.Movement.HasPath && PathCompleted;
        private bool NoPathAndMove => !_body.Movement.HasPath 
            && (PathCompleted || (_body.Movement.Velocity.sqrMagnitude <= 0.01f && _body.Movement.Acceleration <= 0.01f));
        private bool PathCompleted => _body.Movement.RemainingDistance <= StoppingDistance;

        public EnemyMovement(Enemy enemy, EnemyBody body, GamePause pause)
        {
            _enemy = enemy;
            _body = body;
            _pause = pause;
            _token = new CancellationTokenSource();

            _enemy.OnInitialized += Reset;
            _enemy.OnActiveChanged += Reset;
        }

        public void Dispose()
        {
            _enemy.OnInitialized -= Reset;
            _enemy.OnActiveChanged -= Reset;
            _token.CancelAndDispose();
        }
        
        public void Tick()
        {
            if (NeedStop)
            {
                _enemy.StopMovement();
            }
        }

        public void MoveTo(Vector3 destination)
        {
            StopChase();
            _enemy.MoveTo(destination);
        }

        public void ChaseTo(ITransformable target)
        {
            if (!_enemy.Model.Destroyed)
            {
                _target = target;

                _token = _token.Refresh();
                _ = UpdateChase(_token.Token);
            }
        }

        public void StopChase()
        {
            _token.TryCancel();
            _target = null;
        }

        private void Reset()
        {
            StopChase();
        }

        private async UniTask UpdateChase(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _target != null)
            {
                if (!_pause.Paused)
                {
                    _enemy.MoveTo(_target.Position);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Model.RepathTime), cancellationToken: token);
            }
        }
    }
}
