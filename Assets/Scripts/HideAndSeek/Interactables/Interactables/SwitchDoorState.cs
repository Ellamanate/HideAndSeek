using Cysharp.Threading.Tasks;
using DG.Tweening;
using HideAndSeek.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class SwitchDoorState : BaseInteraction, IInteractable<Player>, ILimitingReuseAction, 
        IResettable, IAnimatableInteraction<Player>
    {
        [SerializeField] private Door[] _doors;
        [SerializeField] private LimitInteract _defaultInteractLimits;
        [SerializeField] private float _interactionTime = 2;
        [SerializeField] private bool _openAtStart;
        [SerializeField] private ReuseActionRule _reuseActionRule;
        [SerializeField] private InvokeEvents _events;

        public bool Opened { get; protected set; }

        private ProgressUIService _progressUIService;
        private CancellationTokenSource _token = new CancellationTokenSource();

        public IReadOnlyCollection<Door> Doors;

        public LimitInteract LimitInteract { get; private set; }
        public bool TouchTrigger => false;

        public ReuseActionRule ReuseActionRule => _reuseActionRule;
        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => transform.position;

        [Inject]
        private void Construct(ProgressUIService progressUIService)
        {
            _progressUIService = progressUIService;
            Doors = Array.AsReadOnly(_doors);

            foreach (var door in _doors)
            {
                door.OnStateChanged += OnDoorStateChanged;
            }

            Clear();
            OnConstruct();
        }

        private void Awake()
        {
            SetOpeningState();
        }

        private void OnDestroy()
        {
            _token.CancelAndDispose();
            
            foreach (var door in _doors)
            {
                door.OnStateChanged -= OnDoorStateChanged;
            }
        }

        public void Interact(Player player)
        {
            OnInteract(player);
            InvokeEvents();
        }

        public void ToDefault()
        {
            Clear();
            SetOpeningState();
        }

        public async UniTask PlayInteractionAnimation(Player player, CancellationToken token)
        {
            var progress = _progressUIService.AddProgressTo(ProgressUIType.Circle, player.UpdateBody.Transform, player.Model.UIOffset);
            float progressValue = 0;

            var tween = DOTween.To(() => progressValue, SetProgress, 1, _interactionTime);
            tween.SetEase(Ease.Linear);

            try
            {
                await tween.AsyncWaitForKill(token);
            }
            finally
            {
                _progressUIService.RemoveProgress(progress);
            }

            void SetProgress(float currentProgressValue)
            {
                progressValue = currentProgressValue;
                progress.SetProgress(currentProgressValue);
            }
        }

        protected virtual void OnOpened() { }
        protected virtual void OnClosed() { }
        protected virtual void OnConstruct() { }
        protected virtual void OnDoorStateChanged() { }

        protected virtual void OnInteract(Player player) 
        {
            if (Opened)
            {
                CloseDoors();
            }
            else
            {
                OpenDoors();
            }
        }

        protected void SetOpeningState()
        {
            if (Opened)
            {
                SetOpened();
            }
            else
            {
                SetClosed();
            }
        }

        protected void SetClosed()
        {
            Opened = false;

            foreach (var door in _doors)
            {
                door.SetClose();
            }

            OnClosed();
        }

        protected void SetOpened()
        {
            Opened = true;

            foreach (var door in _doors)
            {
                door.SetOpen();
            }

            OnOpened();
        }

        protected void CloseDoors()
        {
            Opened = false;

            foreach (var door in _doors)
            {
                _ = door.Close(_token.Token);
            }

            OnClosed();
        }

        protected void OpenDoors()
        {
            Opened = true;

            foreach (var door in _doors)
            {
                _ = door.Open(_token.Token);
            }

            OnOpened();
        }

        private void InvokeEvents()
        {
            if (_events != null && _events.CanInvoke())
            {
                _events.Invoke();
            }
        }

        private void Clear()
        {
            LimitInteract = new LimitInteract
            {
                CanPlayerInteract = _defaultInteractLimits.CanPlayerInteract,
                CanEnemyInteract = _defaultInteractLimits.CanEnemyInteract
            };

            Opened = _openAtStart;
        }
    }
}