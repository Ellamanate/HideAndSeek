using System;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerInput : IDisposable, IFixedTickable
    {
        private readonly Player _player;
        private readonly PlayerUpdateBody _updateBody;
        private readonly PlayerInteract _interact;
        private readonly InputSystem _inputSystem;

        public bool Active { get; private set; }

        public PlayerInput(Player player, PlayerUpdateBody updateBody, PlayerInteract interact, InputSystem inputSystem)
        {
            _player = player;
            _updateBody = updateBody;
            _interact = interact;
            _inputSystem = inputSystem;

            _inputSystem.OnInteract += Interact;
        }

        public void Dispose()
        {
            _inputSystem.OnInteract -= Interact;
        }

        public void FixedTick()
        {
            if (Active && _updateBody.Available && _player.Model.Visible)
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
            _interact.Interact(_player);
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_player.Model.Right) * side + ProjectOnFloor(_player.Model.Forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _player.Model.Up).normalized;
        }
    }
}
