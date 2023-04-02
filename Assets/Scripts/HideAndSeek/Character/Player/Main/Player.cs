using System;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class Player : IDisposable, ITickable, IDestroyable, ITransformable
    {
        public event Action<IInteractable> OnInteractableEnter;
        public event Action<IInteractable> OnInteractableExit;
        public event Action OnDestroyed;

        public readonly PlayerModel Model;

        private PlayerBody _body;
        
        public bool Available => !Model.Destroyed && _body != null;
        public Vector3 Position => Model.Position;
        public Quaternion Rotation => Model.Rotation;

        public Player(PlayerModel model)
        {
            Model = model;
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
                GameLogger.Log("Player already has body");
                return;
            }

            _body = body;

            SetSpeed(Model.Speed);

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

        public void Tick()
        {
            if (Available)
            {
                Model.Position = _body.transform.position;
                Model.Rotation = _body.transform.rotation;
            }
        }

        public bool HittedBody(RaycastHit hit)
        {
            return Available && _body.HittedBody(hit);
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (Available)
            {
                _body.Movement.SetVelocity(velocity);
            }
        }

        public void SetSpeed(float speed)
        {
            if (Available)
            {
                _body.Movement.SetSpeed(speed);
                Model.Speed = speed;
            }
        }

        public void SetPosition(Vector3 position)
        {
            if (Available)
            {
                _body.Movement.SetPosition(position);
                Model.Position = position;
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            if (Available)
            {
                _body.Movement.SetRotation(rotation);
                Model.Rotation = rotation;
            }
        }

        private void DestroyPlayer()
        {
            Model.Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        private void InteractableEnter(IInteractable interactable) => OnInteractableEnter?.Invoke(interactable);
        private void InteractableExit(IInteractable interactable) => OnInteractableExit?.Invoke(interactable);
    }
}