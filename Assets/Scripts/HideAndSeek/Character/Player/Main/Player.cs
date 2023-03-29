using System;
using Infrastructure;
using UnityEngine;

namespace HideAndSeek
{
    public class Player : IDisposable, IDestroyable
    {
        public event Action<ITrigger> OnTriggerEnter;
        public event Action<ITrigger> OnTriggerExit;
        public event Action OnDestroyed;

        private PlayerBody _body;

        public Transform Transform => _body.transform;
        public bool Destroyed { get; private set; }
        
        public bool CanUpdate => !Destroyed;
        
        public void Dispose()
        {
            _body.OnDestroyed -= DestroyPlayer;
            _body.TriggerSensor.OnEnter -= EnterTrigger;
            _body.TriggerSensor.OnExit -= ExitTrigger;
        }

        public void Initialize(PlayerBody body)
        {
            if (_body != null)
            {
                GameLogger.Log("Player already has body");
                return;
            }

            _body = body;

            _body.OnDestroyed += DestroyPlayer;
            _body.TriggerSensor.OnEnter += EnterTrigger;
            _body.TriggerSensor.OnExit += ExitTrigger;
        }

        public void Destroy()
        {
            if (!Destroyed)
            {
                _body.Destroy();
            }
        }

        public void SetMovementDirection(Vector3 velocity)
        {
            if (CanUpdate)
            {
                _body.Movement.SetMovementDirection(velocity);
            }
        }

        public void SetPosition(Vector3 position) => _body.Movement.SetPosition(position);
        public void SetRotation(Quaternion rotation) => _body.Movement.SetRotation(rotation);

        private void DestroyPlayer()
        {
            Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        private void EnterTrigger(ITrigger trigger) => OnTriggerEnter?.Invoke(trigger);
        private void ExitTrigger(ITrigger trigger) => OnTriggerExit?.Invoke(trigger);
    }
}