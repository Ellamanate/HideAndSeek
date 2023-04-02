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
        private readonly GamePause _pause;

        private CancellationTokenSource _token;
        private ITransformable _target;

        public EnemyMovement(Enemy enemy, GamePause pause)
        {
            _enemy = enemy;
            _pause = pause;
            _token = new CancellationTokenSource();

            _enemy.OnReseted += Reset;
            _enemy.OnActiveChanged += Reset;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
            _enemy.OnReseted -= Reset;
            _enemy.OnActiveChanged -= Reset;
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
