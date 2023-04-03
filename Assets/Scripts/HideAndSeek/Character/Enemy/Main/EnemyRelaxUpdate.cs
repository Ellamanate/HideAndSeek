using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.AI;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class EnemyRelaxUpdate : IDisposable
    {
        private readonly Enemy _enemy;

        private CancellationTokenSource _token;

        public EnemyRelaxUpdate(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.OnAttentivenesChanged += ChangeAttentiveness;
            _enemy.OnReseted += Reset;
        }

        public void Dispose()
        {
            _enemy.OnAttentivenesChanged -= ChangeAttentiveness;
            _enemy.OnReseted -= Reset;
            _token.CancelAndDispose();
        }

        private void Reset()
        {
            _token.TryCancel();
        }

        private void ChangeAttentiveness()
        {
            if (_enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching)
            {
                _token = _token.Refresh();
                _ = Timer(AttentivenessType.Relax, _token.Token);
            }
        }

        private async UniTask Timer(AttentivenessType targetType, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5), cancellationToken: token);

            _enemy.SetAttentiveness(targetType);
        }
    }
}
