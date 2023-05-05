using UnityEngine;

namespace HideAndSeek
{
    public interface IPositionedInteraction
    {
        public Vector3 Position { get; }
        public Vector3 InteractionPosition { get; }
    }
}
