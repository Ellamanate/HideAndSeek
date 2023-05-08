using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace HideAndSeek
{
    public class InteractionLocker : IDisposable
    {
        private TimeLock _timeLock;
        private RepetitionsLock _repetitionsLock;

        public bool TimeLocked { get; private set; }
        public bool ActionsLocked { get; private set; }

        public bool Locked => TimeLocked || (_repetitionsLock != null && _repetitionsLock.Locked);

        public void Dispose()
        {
            _timeLock?.Dispose();
            TimeLocked = false;
            ActionsLocked = false;
        }

        public void LockByRepetitions(int maxRepetitions)
        {
            if (_repetitionsLock == null)
            {
                _repetitionsLock = new RepetitionsLock(maxRepetitions);
            }

            _repetitionsLock.Increment();
        }

        public async UniTask LockByTime(float time, CancellationToken token)
        {
            if (_timeLock == null)
            {
                _timeLock = new TimeLock();
            }

            TimeLocked = true;

            await _timeLock.WaitUnlock(time, token);

            TimeLocked = false;
        }
    }
}
