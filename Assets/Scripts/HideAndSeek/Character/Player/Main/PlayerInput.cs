using System;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerInput : IDisposable, IFixedTickable
    {
        private readonly PlayerModel _model;
        private readonly PlayerUpdateBody _updateBody;
        private readonly PlayerInteract _interact;
        private readonly HidePlayer _hidePlayer;
        private readonly InputSystem _inputSystem;

        public bool Active { get; private set; }

        public PlayerInput(PlayerModel model, PlayerUpdateBody updateBody, PlayerInteract interact, HidePlayer hidePlayer, InputSystem inputSystem)
        {
            _model = model;
            _updateBody = updateBody;
            _interact = interact;
            _hidePlayer = hidePlayer;
            _inputSystem = inputSystem;

            _inputSystem.OnInteract += Interact;
        }

        public void Dispose()
        {
            _inputSystem.OnInteract -= Interact;
        }

        public void FixedTick()
        {
            if (Active && _updateBody.Available && _model.Visible)
            {
                _updateBody.SetVelocity(CalculateMovementDirection(_inputSystem.MovementUp,
                    _inputSystem.MovementSide));
            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        private void Interact()
        {
            if (!_model.Visible)
            {
                _hidePlayer.Show();
                return;
            }

            _interact.Interact();
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_model.Right) * side + ProjectOnFloor(_model.Forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _model.Up).normalized;
        }
    }
}
