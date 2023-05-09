using HideAndSeek.Utils;
using System.Threading;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class SwitchDoorState : MonoBehaviour, IInteractable<Player>, ILimitingReuseAction, IResettable
    {
        [SerializeField] private Door _door;
        [SerializeField] private LimitInteract _defaultInteractLimits;
        [SerializeField] private ReuseActionRule _reuseActionRule;

        private CancellationTokenSource _token = new CancellationTokenSource();

        public LimitInteract LimitInteract { get; private set; }
        public bool TouchTrigger => false;

        public ReuseActionRule ReuseActionRule => _reuseActionRule;
        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => transform.position;

        [Inject]
        private void Construct()
        {
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
    }
}