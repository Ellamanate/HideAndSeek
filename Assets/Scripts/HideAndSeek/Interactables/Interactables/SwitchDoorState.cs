using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class SwitchDoorState : MonoBehaviour, IInteractable<Player>, IResettable
    {
        [SerializeField] private Door _door;
        [SerializeField] private LimitInteract _defaultInteractLimits;

        public LimitInteract LimitInteract { get; private set; }
        public bool TouchTrigger => false;

        [Inject]
        private void Construct()
        {
            ToDefault();
        }

        public void Interact(Player player)
        {
            if (_door.Opened)
            {
                _ = _door.Close();
            }
            else
            {
                _ = _door.Open();
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