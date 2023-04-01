using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyMovement : IDisposable
    {
        private readonly Enemy _enemy;

        private CancellationTokenSource _token;
        private ITransformable _target;

        public EnemyMovement(Enemy enemy)
        {
            _enemy = enemy;
            _token = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void MoveTo(Vector3 destination)
        {
            StopChase();
            _enemy.MoveTo(destination);
        }

        public void ChaseTo(ITransformable target)
        {
            if (_enemy.Available)
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

        private async UniTask UpdateChase(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _target != null)
            {
                _enemy.MoveTo(_target.Position);
                await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Model.RepathTime), cancellationToken: token);
            }
        }
    }
}
