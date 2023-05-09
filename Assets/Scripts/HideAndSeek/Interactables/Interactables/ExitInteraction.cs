using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class ExitInteraction : MonoBehaviour, IInteractable<Player>
    {
        [field: SerializeField] public bool TouchTrigger { get; private set; } = true;

        public LimitInteract LimitInteract { get; private set; }

        private MainGame _game;

        public Vector3 Position => transform.position;
        public Vector3 InteractionPosition => transform.position;

        [Inject]
        private void Construct(MainGame game)
        {
            _game = game;
            LimitInteract = new LimitInteract 
            { 
                CanPlayerInteract = true, 
                CanEnemyInteract = false
            };
        }

        public void Interact(Player player)
        {
            _game.CompleteGame();
        }
    }
}