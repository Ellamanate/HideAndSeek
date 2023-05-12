using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerInteract : BaseInteract<Player>
    {
        private readonly PlayerModel _model;
        private readonly PlayerUpdateBody _updateBody;
        private readonly PlayerHUD _hud;
        private readonly HidePlayer _hidePlayer;

        public IInteractable<Player> CurrentInteractable { get; private set; }

        public PlayerInteract(PlayerModel model, PlayerUpdateBody updateBody, PlayerHUD hud, HidePlayer hidePlayer)
        {
            _model = model;
            _updateBody = updateBody;
            _hud = hud;
            _hidePlayer = hidePlayer;
        }

        protected override InteractorType CurrentInteractorType => InteractorType.Player;

        public override bool CheckInteractionAvailable(IInteractable<Player> interactable)
        {
            return interactable.LimitInteract.CanPlayerInteract
                && Physics.Raycast(_updateBody.RaycastPosition, interactable.Position - _updateBody.RaycastPosition, 
                    out var hitInfo, _model.RaycastDistance, _model.RaycastLayers)
                && interactable.Hitted(hitInfo);
        }

        public override void Interact(Player player)
        {
            if (_hidePlayer.HasShelter)
            {
                player.UpdateBody.SetPosition(_hidePlayer.CurrentShelter.InteractionPosition);
                player.UpdateBody.SetVelocity(Vector3.zero);

                _hidePlayer.Show();
                UpdateCurrentInteraction();
            }
            else if (CurrentInteractable != null)
            {
                if (CurrentInteractable is Shelter)
                {
                    InteractAndLock(player, CurrentInteractable);
                    _hud.ShowInteraction(CurrentInteractable);
                }
                else
                {
                    InteractAndLock(player, CurrentInteractable);
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
                CurrentInteractable = null;
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
            CurrentInteractable = Interactables
                .OrderBy(x => Vector3.Distance(x.Position, _model.Position))
                .FirstOrDefault(IsInteractableValid);
        }

        private void UpdateIcon()
        {
            if (CurrentInteractable != null)
            {
                _hud.ShowInteraction(CurrentInteractable);
            }
            else
            {
                _hud.HideInteraction();
            }
        }
    }
}
