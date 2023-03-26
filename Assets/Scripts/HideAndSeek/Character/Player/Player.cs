using System;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class Player : IDisposable, IDestroyable
    {
        public event Action OnDestroyed;

        private readonly PlayerBody _body;

        public bool Active { get; private set; }
        public bool Destroyed { get; private set; }

        public Transform Transform => _body.transform;

        public Player(PlayerBody body)
        {
            _body = body;
            _body.OnDestroyed += OnBodyDestroyed;
        }

        public void Dispose()
        {
            _body.OnDestroyed -= OnBodyDestroyed;
        }

        public void Destroy()
        {
            if (!Destroyed)
            {
                _body.Destroy();
            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        public void SetMovementDirection(Vector3 velocity)
        {
            if (Active)
            {
                _body.Movement.SetMovementDirection(velocity);
            }
        }

        private void OnBodyDestroyed()
        {
            Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        public class Factory : PlaceholderFactory<PlayerBody, Player> { }
    }
}