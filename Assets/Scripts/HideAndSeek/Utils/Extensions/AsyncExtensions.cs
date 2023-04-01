using System.Threading;

namespace HideAndSeek.Utils
{
    public static class AsyncExtensions
    {
        public static CancellationTokenSource Refresh(this CancellationTokenSource token)
        {
            if (token != null)
            {
                token.CancelAndDispose();
            }

            return new CancellationTokenSource();
        }

        public static void CancelAndDispose(this CancellationTokenSource token)
        {
            if (token == null) return;

            token.TryCancel();
            token.Dispose();
        }

        public static void TryCancel(this CancellationTokenSource token)
        {
            if (token == null) return;

            if (!token.IsCancellationRequested)
            {
                token.Cancel(true);
            }
        }
    }
}
