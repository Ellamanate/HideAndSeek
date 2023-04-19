using System;
using HideAndSeek.AI;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class Enemy : IDisposable, ITickable, IDestroyable, ITransformable
    {
        public event Action OnInitialized;
        public event Action OnDestroyed;
        public event Action OnActiveChanged;
        public event Action OnAttentivenesChanged;
        public event Action OnStopped;
        public event Action OnDestinationChanged;
        public event Action<IInteractable> OnInteractableEnter;
        public event Action<IInteractable> OnInteractableExit;

        public readonly EnemyModel Model;

        private readonly EnemyBody _body;
        private readonly GamePause _pause;
        private readonly Actions<OrderActionType> _actions;
        private readonly Execution<OrderActionType, OrderCounterType> _execution;

        public Enemy(EnemyModel model, EnemyBody body, Actions<OrderActionType> actions,
            Execution<OrderActionType, OrderCounterType> execution, GamePause pause)
        {
            Model = model;
            _body = body;
            _actions = actions;
            _execution = execution;
            _pause = pause;

            _body.OnDestroyed += DestroyEnemy;
            _body.InteractableTrigger.OnEnter += InteractableEnter;
            _body.InteractableTrigger.OnExit += InteractableExit;
        }

        public Vector3 RaycastPosition => _body.RaycastPosition.position;
        public Vector3 Position => Model.Position;
        public Quaternion Rotation => Model.Rotation;

        public void Dispose()
        {
            _body.OnDestroyed -= DestroyEnemy;
            _body.InteractableTrigger.OnEnter -= InteractableEnter;
            _body.InteractableTrigger.OnExit -= InteractableExit;
        }

        public void Destroy()
        {
            if (!Model.Destroyed)
            {
                _body.Destroy();
            }
        }

        public void Initialize()
        {
            _actions.Initialize(Model.ActionTypes);
            _execution.Initialize(Model.CounterTypes);
            _body.Movement.SetMaxSpeed(Model.Speed);
            _body.VisionCone.SetConeData(Model.ConeData);
            _body.VisionCone.Initialize();

            SetPosition(Model.Position);
            SetRotation(Model.Rotation);
            Stop();

            OnInitialized?.Invoke();
        }

        public void Tick()
        {
            if (!Model.Destroyed && !_pause.Paused)
            {
                Model.Position = _body.transform.position;
                Model.Rotation = _body.transform.rotation;
            }
        }

        public void SetParent(Transform parent)
        {
            _body.transform.SetParent(parent);
        }

        public void SetActive(bool active)
        {
            if (!Model.Destroyed && Model.Active != active)
            {
                Model.Active = active;

                if (!active)
                {
                    StopMovement();
                }

                OnActiveChanged?.Invoke();
            }
        }

        public void UpdateAction()
        {
            if (!Model.Destroyed && Model.Active)
            {
                _execution.Execute();
            }
        }

        public void SetAttentiveness(AttentivenessType attentiveness)
        {
            if (!Model.Destroyed && Model.Active && attentiveness != Model.CurrentAttentiveness)
            {
                UpdateAttentiveness(attentiveness);
                OnAttentivenesChanged?.Invoke();
            }
        }

        public void SetPosition(Vector3 position)
        {
            _body.Movement.Warp(position);
            Model.Position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _body.Movement.SetRotation(rotation);
            Model.Rotation = rotation;
        }

        public void MoveTo(Vector3 destination)
        {
            if (!Model.Destroyed && Model.Active)
            {
                _body.Movement.MoveTo(destination);
                Model.Destination = destination;
                Model.Moved = true;
                OnDestinationChanged?.Invoke();
            }
        }

        public void StopMovement()
        {
            if (!Model.Destroyed)
            {
                Stop();
                OnStopped?.Invoke();
            }
        }

        private void Stop()
        {
            _body.Movement.Stop();
            Model.Moved = false;
            GameLogger.Log("Enemy stopped");
        }

        private void UpdateAttentiveness(AttentivenessType attentiveness)
        {
            Model.CurrentAttentiveness = attentiveness;
            _body.Movement.SetMaxSpeed(Model.Speed);
            _body.VisionCone.SetConeData(Model.ConeData);
            GameLogger.Log($"Attentiveness: {attentiveness}");
        }

        private void DestroyEnemy()
        {
            Model.Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        private void InteractableEnter(IInteractable interactable) => OnInteractableEnter?.Invoke(interactable);
        private void InteractableExit(IInteractable interactable) => OnInteractableExit?.Invoke(interactable);

        public class Factory : PlaceholderFactory<EnemyModel, EnemyBody, EnemySceneConfig, Enemy> { }
    }
}
