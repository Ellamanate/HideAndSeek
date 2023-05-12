using UnityEngine;

namespace HideAndSeek
{
    public class PlayerModel : CharacterModel
    {
        public Vector3 UIOffset;
        public bool Visible;
        public float Speed;
        public float RaycastDistance;
        public int RaycastLayers;
    }
}
