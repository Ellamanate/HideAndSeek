using System;
using Zenject;

namespace HideAndSeek
{
    public class Enemy : IDisposable, ITickable, IDestroyable
    {
        public event Action OnDestroyed;

        public readonly EnemyModel Model;

        private readonly EnemyBody _body;

        public Enemy(EnemyModel model, EnemyBody body)
        {
            Model = model;
            _body = body;
            _body.OnDestroyed += DestroyEnemy;
        }

        public bool Available => !Model.Destroyed;

        public void Dispose()
        {
            _body.OnDestroyed -= DestroyEnemy;
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

        public void SetSpeed(float speed)
        {
            if (Available)
            {
                _body.Movement.SetSpeed(speed);
                Model.Speed = speed;
            }
        }

        private void DestroyEnemy()
        {
            Model.Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        public class Factory : PlaceholderFactory<EnemyModel, EnemyBody, Enemy> { }
    }
}
