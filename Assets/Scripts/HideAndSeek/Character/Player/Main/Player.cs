using System;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class Player : IDisposable, ITickable, IDestroyable
    {
        public event Action<ITrigger> OnTriggerEnter;
        public event Action<ITrigger> OnTriggerExit;
        public event Action OnDestroyed;

        public readonly PlayerModel Model;

        private PlayerBody _body;
        
        public bool Available => !Model.Destroyed && _body != null;

        public Player(PlayerModel model)
        {
            Model = model;
        }

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

        public void MoveToDirection(Vector3 direction)
        {
            if (Available)
            {
                _body.Movement.MoveToDirection(direction);
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

        private void EnterTrigger(ITrigger trigger) => OnTriggerEnter?.Invoke(trigger);
        private void ExitTrigger(ITrigger trigger) => OnTriggerExit?.Invoke(trigger);
    }
}