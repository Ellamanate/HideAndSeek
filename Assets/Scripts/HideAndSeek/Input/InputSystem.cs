using Rewired;
using System;
using Zenject;

namespace HideAndSeek
{
    public class InputSystem : IDisposable, IInitializable
    {
        public event Action OnInteract;
        public event Action OnBack;

        private Rewired.Player _player;
        private bool _initialized;

        public float MovementUp => _player.GetAxis("MovementUp") * Sensitivity;
        public float MovementSide => _player.GetAxis("MovementSide") * Sensitivity;
        public float Sensitivity { get; set; } = 1;

        public void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                _player = ReInput.players.GetPlayer(0);

                _player.AddInputEventDelegate(OnBackButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");
                _player.AddInputEventDelegate(OnInteractButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Interaction");
            }
        }

        public void Dispose()
        {
            _player.RemoveInputEventDelegate(OnBackButtonDown);
            _player.RemoveInputEventDelegate(OnInteractButtonDown);
        }

        private void OnInteractButtonDown(InputActionEventData data) => OnInteract?.Invoke();
        private void OnBackButtonDown(InputActionEventData data) => OnBack?.Invoke();
    }
}