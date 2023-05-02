using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class ChangePatrolZone : MonoBehaviour, IInteractableForPlayer, IResettable
    {
        [SerializeField] private PatrolQueue _targetQueue;

        public bool CanPlayerInteract { get; private set; } = true;

        private ChangePatrolPoints _changePatrol;

        public bool TouchTrigger => true;

        [Inject]
        private void Construct(ChangePatrolPoints changePatrol)
        {
            _changePatrol = changePatrol;
        }

        public void Interact(Player player)
        {
            _changePatrol.SetQueue(_targetQueue);
            CanPlayerInteract = false;
        }

        public void ToDefault()
        {
            CanPlayerInteract = true;
        }
    }
}