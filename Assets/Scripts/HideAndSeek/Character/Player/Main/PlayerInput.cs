using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using System;
using System.Threading;
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

        private CancellationTokenSource _token;
        private bool _animating;

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
            _token.CancelAndDispose();
            _inputSystem.OnInteract -= Interact;
        }

        public void ToDefault()
        {
            _token.TryCancel();
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
            if (_animating)
            {
                _token.TryCancel();
            }
            else
            {
                if (_interact.CurrentInteractable is IAnimatableInteraction<Player> animatable)
                {
                    _token = _token.Refresh();
                    _ = InteractAnimation(animatable, _player, _token.Token);
                }
                else
                {
                    _token.TryCancel();
                    _interact.Interact(_player);
                }
            }
        }

        private Vector3 CalculateMovementDirection(float up, float side)
        {
            var direction = ProjectOnFloor(_player.Model.Right) * side + ProjectOnFloor(_player.Model.Forward) * up;

            return direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 ProjectOnFloor(Vector3 direction) => Vector3.ProjectOnPlane(direction, _player.Model.Up).normalized;
        }

        private async UniTask InteractAnimation(IAnimatableInteraction<Player> animatable,
            Player player, CancellationToken token)
        {
            SetActive(false);
            _animating = true;
            player.UpdateBody.SetVelocity(Vector3.zero);

            try
            {
                await animatable.PlayInteractionAnimation(player, token);

                _interact.Interact(_player);
            }
            finally
            {
                SetActive(true);
                _animating = false;
            }
        }
    }
}
