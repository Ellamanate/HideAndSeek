using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using System;
using System.Threading;

namespace HideAndSeek
{
    public class TimeLock : IDisposable
    {
        private CancellationTokenSource _token;

        public async UniTask WaitUnlock(float time, CancellationToken token)
        {
            _token = _token.Refresh();
            _token.AddTo(token);
            await Timer(time, _token.Token);
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        private async UniTask Timer(float time, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
        }
    }
}
