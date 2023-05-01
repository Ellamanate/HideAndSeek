using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class Shelter : MonoBehaviour, IInteractableForPlayer, IInteractableForEnemy, IResettable
    {
        [SerializeField] private Transform EnemyInteractPoint;

        [field: SerializeField] public bool TouchTrigger { get; private set; }

        public bool CanPlayerInteract { get; private set; } = true;
        public bool CanEnemyInteract { get; private set; } = true;

        private FailGame _failGame;
        private HidePlayer _hidePlayer;

        [Inject]
        private void Construct(FailGame failGame, HidePlayer hidePlayer)
        {
            _failGame = failGame;
            _hidePlayer = hidePlayer;
        }

        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => EnemyInteractPoint.position;

        public void Interact(Player player)
        {
            _hidePlayer.Hide(this);
        }

        public void Interact(Enemy interactor)
        {
            CanEnemyInteract = false;

            if (_hidePlayer.CurrentShelter == this)
            {
                _failGame.SetFail();
            }
        }

        public void ToDefault()
        {
            CanPlayerInteract = true;
            CanEnemyInteract = true;
        }
    }
}