using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek
{
    public class PlayerInteract
    {
        private readonly PlayerHUD _hud;

        private List<IInteractable<Player>> _interactables;

        public PlayerInteract(PlayerHUD hud)
        {
            _hud = hud;
            _interactables = new List<IInteractable<Player>>();
        }

        public void Clear()
        {
            _interactables.Clear();
        }

        public void Interact(Player player)
        {
            if (_interactables.Count > 0)
            {
                var interaction = GetValidInteraction();
                interaction?.Interact(player);
            }
        }

        public void AddInteractable(Player player, IInteractable<Player> interactable)
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

        public void RemoveInteractable(IInteractable<Player> interactable)
        {
            _interactables.Remove(interactable);

            if (_interactables.Count == 0)
            {
                _hud.HideInteractionIcon();
            }
        }

        private IInteractable<Player> GetValidInteraction()
        {
            return _interactables.FirstOrDefault(x => x.LimitInteract.CanPlayerInteract);
        }
    }
}
