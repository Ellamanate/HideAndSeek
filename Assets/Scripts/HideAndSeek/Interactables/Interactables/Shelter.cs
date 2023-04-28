using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class Shelter : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public bool TouchTrigger { get; private set; }

        private HidePlayer _hidePlayer;

        [Inject]
        private void Construct(HidePlayer hidePlayer)
        {
            _hidePlayer = hidePlayer;
        }

        public void Interact()
        {
            _hidePlayer.Hide();
        }
    }
}