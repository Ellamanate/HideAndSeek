using System.Collections.Generic;

namespace HideAndSeek
{
    public class PlayerInteract
    {
        private readonly PlayerHUD _hud;

        private List<IInteractable> _interactables;

        public PlayerInteract(PlayerHUD hud)
        {
            _hud = hud;
            _interactables = new List<IInteractable>();
        }

        public void Clear()
        {
            _interactables.Clear();
        }

        public void Interact()
        {
            if (_interactables.Count > 0)
            {
                _interactables[0].Interact();
            }
        }

        public void AddInteractable(IInteractable interactable)
        {
            if (interactable.TouchTrigger)
            {
                interactable.Interact();
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

        public void RemoveInteractable(IInteractable interactable)
        {
            _interactables.Remove(interactable);

            if (_interactables.Count == 0)
            {
                _hud.HideInteractionIcon();
            }
        }
    }
}
