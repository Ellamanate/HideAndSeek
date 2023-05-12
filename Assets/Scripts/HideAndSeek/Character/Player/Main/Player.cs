using System;

namespace HideAndSeek
{
    public class Player : IDisposable, IDestroyable
    {
        public event Action OnDestroyed;

        public readonly PlayerModel Model;
        public readonly PlayerUpdateBody UpdateBody;
        public readonly PlayerInteract Interact;
        public readonly PlayerVisibility PlayerVisibility;

        private PlayerBody _body;
        
        public bool Available => !Model.Destroyed && _body != null;

        public Player(PlayerModel model, PlayerUpdateBody updateBody, PlayerInteract interact, PlayerVisibility playerVisibility)
        {
            Model = model;
            UpdateBody = updateBody;
            Interact = interact;
            PlayerVisibility = playerVisibility;
        }

        public void Dispose()
        {
            _body.OnDestroyed -= DestroyPlayer;
            _body.InteractableTrigger.OnEnter -= InteractableEnter;
            _body.InteractableTrigger.OnExit -= InteractableExit;
        }

        public void Initialize(PlayerBody body)
        {
            if (_body != null)
            {
                GameLogger.Log("Player already have body");
                return;
            }

            _body = body;

            PlayerVisibility.Initialize(_body);
            UpdateBody.Initialize(_body);
            UpdateBody.SetSpeed(Model.Speed);

            _body.OnDestroyed += DestroyPlayer;
            _body.InteractableTrigger.OnEnter += InteractableEnter;
            _body.InteractableTrigger.OnExit += InteractableExit;
        }

        public void Destroy()
        {
            if (!Model.Destroyed)
            {
                _body.Destroy();
            }
        }

        private void DestroyPlayer()
        {
            Model.Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        private void InteractableEnter(IInteractable<Player> interactable)
        {
            Interact.AddInteractable(this, interactable);
        }

        private void InteractableExit(IInteractable<Player> interactable)
        {
            Interact.RemoveInteractable(interactable);
        }
    }
}