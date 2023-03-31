using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerInput : IFixedTickable
    {
        private readonly Player _player;
        private readonly InputSystem _inputSystem;

        public bool Active { get; private set; }

        public PlayerInput(Player player, InputSystem inputSystem)
        {
            _player = player;
            _inputSystem = inputSystem;
        }

        public void FixedTick()
        {
            if (Active && _player.Available)
            {
                _player.SetVelocity(CalculateMovementDirection(_inputSystem.MovementUp,
                    _inputSystem.MovementSide));
            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_player.Model.Right) * side + ProjectOnFloor(_player.Model.Forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _player.Model.Up).normalized;
        }
    }
}
