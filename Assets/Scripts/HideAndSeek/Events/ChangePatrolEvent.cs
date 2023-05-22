using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class ChangePatrolEvent : MonoBehaviour, IEvent
    {
        [SerializeField] private PatrolQueue _targetQueue;

        private ChangePatrolPoints _changePatrol;

        [Inject]
        private void Construct(ChangePatrolPoints changePatrol)
        {
            _changePatrol = changePatrol;
        }

        public bool CanInvoke()
        {
            return true;
        }

        public void Invoke()
        {
            _changePatrol.SetQueue(_targetQueue);
        }
    }
}
