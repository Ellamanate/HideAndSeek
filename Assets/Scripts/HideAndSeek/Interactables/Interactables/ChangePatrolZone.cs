﻿using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class ChangePatrolZone : MonoBehaviour, IInteractable<Player>, ILimitingReuseAction, IResettable
    {
        [SerializeField] private PatrolQueue _targetQueue;
        [SerializeField] private ReuseActionRule _limitRule;
        [SerializeField] private LimitInteract _defaultInteractLimits;

        public LimitInteract LimitInteract { get; private set; }

        private ChangePatrolPoints _changePatrol;

        public ReuseActionRule ReuseActionRule => _limitRule;
        public bool TouchTrigger => true;

        [Inject]
        private void Construct(ChangePatrolPoints changePatrol)
        {
            _changePatrol = changePatrol;
            ToDefault();
        }

        public void Interact(Player player)
        {
            _changePatrol.SetQueue(_targetQueue);
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