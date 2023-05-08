﻿using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class Shelter : MonoBehaviour, IInteractable<Player>, IInteractable<Enemy>, IPositionedInteraction, ILimitingReuseTime, IResettable
    {
        [SerializeField] private Transform _enemyInteractPoint;
        [SerializeField] private LimitInteract _defaultInteractLimits;
        [SerializeField] private ReuseTimeRule _timeRule;

        public LimitInteract LimitInteract { get; private set; }
        [field: SerializeField] public bool TouchTrigger { get; private set; }

        private FailGame _failGame;
        private HidePlayer _hidePlayer;

        [Inject]
        private void Construct(FailGame failGame, HidePlayer hidePlayer)
        {
            _failGame = failGame;
            _hidePlayer = hidePlayer;
            ToDefault();
        }

        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => _enemyInteractPoint.position;
        public ReuseTimeRule ReuseTimeRule => _timeRule;

        public void Interact(Player player)
        {
            _hidePlayer.Hide(this);
        }

        public void Interact(Enemy interactor)
        {
            if (_hidePlayer.CurrentShelter == this)
            {
                _failGame.SetFail();
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