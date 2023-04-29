using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class ExitInteraction : MonoBehaviour, IInteractableForPlayer
    {
        [field: SerializeField] public bool TouchTrigger { get; private set; } = true;

        public bool CanPlayerInteract => true;

        private MainGame _game;

        [Inject]
        private void Construct(MainGame game)
        {
            _game = game;
        }

        public void Interact(Player player)
        {
            _game.CompleteGame();
        }
    }
}