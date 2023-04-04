using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class EnemyWakeUp : IDisposable
    {
        private readonly Enemy _enemy;

        private CancellationTokenSource _token;

        public bool EnemySleepd { get; private set; } = true;

        public EnemyWakeUp(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.OnStopped += StartWakeUp;
            _enemy.OnDestinationChanged += StopWakeUp;
            _enemy.OnInitialized += Reset;
        }

        public void Dispose()
        {
            _enemy.OnStopped -= StartWakeUp;
            _enemy.OnDestinationChanged -= StopWakeUp;
            _enemy.OnInitialized -= Reset;
            _token.CancelAndDispose();
        }

        private void StartWakeUp()
        {
            EnemySleepd = true;

            _token = _token.Refresh();
            _ = Timer(_token.Token);
        }

        private void StopWakeUp()
        {
            if (EnemySleepd)
            {
                EnemySleepd = false;
                _token.TryCancel();
            }
        }

        private void Reset()
        {
            EnemySleepd = true;
        }

        private async UniTask Timer(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy.Model.WakeUpTimer), cancellationToken: token);
            
            _enemy.UpdateAction();
        }
    }
}
