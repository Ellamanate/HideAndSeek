using UnityEngine;

namespace HideAndSeek
{
    public class EnemyModel : CharacterModel
    {
        public Vector3 Destination;
        public LayerMask RaycastLayers;
        public float VisionDistance;
        public float RepathTime;
    }
}
