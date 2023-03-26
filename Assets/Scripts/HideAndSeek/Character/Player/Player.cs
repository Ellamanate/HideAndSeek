using System;
using Zenject;

namespace HideAndSeek
{
    public class Player : IDisposable, IFixedTickable, IDestroyable
    {
        public event Action OnDestroyed;

        private readonly PlayerBody _body;

        public bool Active { get; private set; }
        public bool Updating { get; private set; }
        public bool Destroyed { get; private set; }

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

        public void FixedTick()
        {
            if (Active && Updating)
            {

            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        public void SetUpdating(bool updating)
        {
            Updating = updating;
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