using System;

namespace HideAndSeek
{
    public class Player : IDisposable, IDestroyable
    {
        public event Action OnDestroyed;

        public readonly PlayerModel Model;
        public readonly PlayerUpdateBody UpdateBody;
        public readonly PlayerInteract Interact;

        private PlayerBody _body;
        
        public bool Available => !Model.Destroyed && _body != null;

        public Player(PlayerModel model, PlayerUpdateBody updateBody, PlayerInteract interact)
        {
            Model = model;
            UpdateBody = updateBody;
            Interact = interact;
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

        private void InteractableEnter(IInteractable interactable)
        {
            Interact.Interact(interactable);
        }

        private void InteractableExit(IInteractable interactable)
        {

        }
    }
}