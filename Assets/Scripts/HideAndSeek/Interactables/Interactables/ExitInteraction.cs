using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class ExitInteraction : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool TouchTrigger { get; private set; } = true;

        private MainGame _game;

        [Inject]
        private void Construct(MainGame game)
        {
            _game = game;
        }

        public void Interact()
        {
            _game.CompleteGame();
        }
    }
}