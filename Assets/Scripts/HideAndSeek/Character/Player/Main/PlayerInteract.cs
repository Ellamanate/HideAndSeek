using System.Collections.Generic;

namespace HideAndSeek
{
    public class PlayerInteract
    {
        private readonly PlayerHUD _hud;

        private List<IInteractableForPlayer> _interactables;

        public PlayerInteract(PlayerHUD hud)
        {
            _hud = hud;
            _interactables = new List<IInteractableForPlayer>();
        }

        public void Clear()
        {
            _interactables.Clear();
        }

        public void Interact(Player player)
        {
            if (_interactables.Count > 0)
            {
                _interactables[0].Interact(player);
            }
        }

        public void AddInteractable(Player player, IInteractableForPlayer interactable)
        {
            if (interactable.TouchTrigger)
            {
                interactable.Interact(player);
            }
            else
            {
                if (!_interactables.Contains(interactable))
                {
                    _interactables.Add(interactable);
                }

                _hud.ShowInteractionIcon();
            }
        }

        public void RemoveInteractable(IInteractableForPlayer interactable)
        {
            _interactables.Remove(interactable);

            if (_interactables.Count == 0)
            {
                _hud.HideInteractionIcon();
            }
        }
    }
}
