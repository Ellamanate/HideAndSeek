using UnityEngine;

namespace HideAndSeek
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerBody BodyPrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}