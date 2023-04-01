using System;
using HideAndSeek.AI;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class Enemy : IDisposable, ITickable, IDestroyable
    {
        public event Action OnDestroyed;

        public readonly EnemyModel Model;

        private readonly EnemyBody _body;
        private readonly Actions<OrderActionType> _actions;
        private readonly Execution<OrderActionType, OrderCounterType> _execution;

        public Enemy(EnemyModel model, EnemyBody body, Actions<OrderActionType> actions,
            Execution<OrderActionType, OrderCounterType> execution)
        {
            Model = model;
            _body = body;
            _actions = actions;
            _execution = execution;

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

        public void Initialize(OrderActionType actionTypes, OrderCounterType counterTypes)
        {
            _actions.Initialize(actionTypes);
            _execution.Initialize(counterTypes);
        }

        public void Tick()
        {
            if (Available)
            {
                Model.Position = _body.transform.position;
                Model.Rotation = _body.transform.rotation;
            }
        }

        public void UpdateAction()
        {
            _execution.Execute();
        }

        public void SetSpeed(float speed)
        {
            if (Available)
            {
                _body.Movement.SetSpeed(speed);
                Model.Speed = speed;
            }
        }

        public void MoveTo(Vector3 destination)
        {
            if (Available)
            {
                _body.Movement.MoveTo(destination);
                Model.Destination = destination;
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
