﻿using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerInput : IFixedTickable
    {
        private readonly Player _player;
        private readonly InputSystem _inputSystem;

        public PlayerInput(Player player, InputSystem inputSystem)
        {
            _player = player;
            _inputSystem = inputSystem;
        }

        public void FixedTick()
        {
            _player.SetMovementDirection(CalculateMovementDirection(_inputSystem.MovementUp, _inputSystem.MovementSide));
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_player.Transform.right) * side + ProjectOnFloor(_player.Transform.forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _player.Transform.up).normalized;
        }
    }
}
