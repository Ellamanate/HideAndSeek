using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerInput : IFixedTickable
    {
        private readonly PlayerModel _model;
        private readonly PlayerUpdateBody _updateBody;
        private readonly InputSystem _inputSystem;

        public bool Active { get; private set; }

        public PlayerInput(PlayerModel model, PlayerUpdateBody updateBody, InputSystem inputSystem)
        {
            _model = model;
            _updateBody = updateBody;
            _inputSystem = inputSystem;
        }

        public void FixedTick()
        {
            if (Active && _updateBody.Available)
            {
                _updateBody.SetVelocity(CalculateMovementDirection(_inputSystem.MovementUp,
                    _inputSystem.MovementSide));
            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_model.Right) * side + ProjectOnFloor(_model.Forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _model.Up).normalized;
        }
    }
}
