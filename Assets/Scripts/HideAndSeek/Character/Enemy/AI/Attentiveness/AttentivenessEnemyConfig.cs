using UnityEngine;

namespace HideAndSeek.AI
{
    [CreateAssetMenu(menuName = "Configs/AttentivenessEnemyConfig", fileName = "AttentivenessEnemyConfig")]
    public class AttentivenessEnemyConfig : ScriptableObject
    {
        [field: SerializeField] public AttentivenessData Attentiveness { get; private set; }
    }
}