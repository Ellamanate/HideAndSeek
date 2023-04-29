using UnityEngine;

namespace HideAndSeek
{
    public interface IInteractableForEnemy
    {
        public Vector3 Position { get; }
        public Vector3 InteractionPosition { get; }
        public bool CanEnemyInteract { get; }
        public bool TouchTrigger { get; }
        public void Interact(Enemy enemy);
    }
}
