using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerInteract : BaseInteract<Player>
    {
        private readonly PlayerModel _model;
        private readonly PlayerHUD _hud;
        private readonly HidePlayer _hidePlayer;

        private IInteractable<Player> _currentInteractable;

        public PlayerInteract(PlayerModel model, PlayerHUD hud, HidePlayer hidePlayer)
        {
            _model = model;
            _hud = hud;
            _hidePlayer = hidePlayer;
        }

        protected override InteractorType CurrentInteractorType => InteractorType.Player;

        public override bool CheckInteractionAvailable(IInteractable<Player> interactable)
        {
            return interactable.LimitInteract.CanPlayerInteract
                && Physics.Raycast(_model.Position, interactable.Position - _model.Position, 
                    out var hitInfo, _model.RaycastDistance, _model.RaycastLayers)
                && interactable.Hitted(hitInfo);
        }

        public override void Interact(Player player)
        {
            if (_hidePlayer.HasShelter)
            {
                _hidePlayer.Show();
                UpdateCurrentInteraction();
            }
            else if (_currentInteractable != null)
            {
                if (_currentInteractable is Shelter)
                {
                    InteractAndLock(player, _currentInteractable);
                    _hud.ShowInteraction(_currentInteractable);
                }
                else
                {
                    InteractAndLock(player, _currentInteractable);
                    UpdateCurrentInteraction();
                }
            }
        }

        public void UpdateCurrentInteraction()
        {
            SetCurrentInteraction();
            UpdateIcon();
        }

        protected override void OnInteractableAdded()
        {
            UpdateCurrentInteraction();
        }

        protected override void OnInteractableRemoved()
        {
            if (Interactables.Count == 0)
            {
                _currentInteractable = null;
                _hud.HideInteraction();
            }
            else
            {
                UpdateCurrentInteraction();
            }
        }

        protected override void OnLockTimeEnded()
        {
            UpdateCurrentInteraction();
        }

        private void SetCurrentInteraction()
        {
            _currentInteractable = Interactables
                .OrderBy(x => Vector3.Distance(x.Position, _model.Position))
                .FirstOrDefault(IsInteractableValid);
        }

        private void UpdateIcon()
        {
            if (_currentInteractable != null)
            {
                _hud.ShowInteraction(_currentInteractable);
            }
            else
            {
                _hud.HideInteraction();
            }
        }
    }
}
