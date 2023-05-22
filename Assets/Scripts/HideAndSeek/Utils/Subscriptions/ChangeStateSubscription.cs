using System;

namespace HideAndSeek.Utils
{
    public class ChangeStateSubscription : ISubscription
    {
        private readonly IStateChanging _stateChanging;
        private readonly Action _onStateChanged;

        public ChangeStateSubscription(IStateChanging stateChanging, Action onStateChanged)
        {
            _stateChanging = stateChanging;
            _onStateChanged = onStateChanged;

            _stateChanging.OnStateChanged += _onStateChanged;
        }

        public void Unsubscribe()
        {
            _stateChanging.OnStateChanged -= _onStateChanged;
        }
    }
}
