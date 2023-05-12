using Cysharp.Threading.Tasks;
using DG.Tweening;
using HideAndSeek.Utils;
using System.Threading;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class SwitchDoorState : BaseInteraction, IInteractable<Player>, ILimitingReuseAction, 
        IResettable, IAnimatableInteraction<Player>
    {
        [SerializeField] private Door _door;
        [SerializeField] private LimitInteract _defaultInteractLimits;
        [SerializeField] private float _interactionTime = 2;
        [SerializeField] private ReuseActionRule _reuseActionRule;

        private ProgressUIService _progressUIService;
        private CancellationTokenSource _token = new CancellationTokenSource();

        public LimitInteract LimitInteract { get; private set; }
        public bool TouchTrigger => false;

        public ReuseActionRule ReuseActionRule => _reuseActionRule;
        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => transform.position;

        [Inject]
        private void Construct(ProgressUIService progressUIService)
        {
            _progressUIService = progressUIService;
            ToDefault();
        }

        private void OnDestroy()
        {
            _token.CancelAndDispose();
        }

        public void Interact(Player player)
        {
            if (_door.Opened)
            {
                _ = _door.Close(_token.Token);
            }
            else
            {
                _ = _door.Open(_token.Token);
            }
        }

        public void ToDefault()
        {
            LimitInteract = new LimitInteract
            {
                CanPlayerInteract = _defaultInteractLimits.CanPlayerInteract,
                CanEnemyInteract = _defaultInteractLimits.CanEnemyInteract
            };
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
    }
}