using HideAndSeek;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/BootstrapConfig", fileName = "BootstrapConfig")]
    public class BootstrapConfig : ScriptableObject
    {
        [field: SerializeField] public bool StartFromMenu { get; private set; }
        [field: SerializeField, HideIf(nameof(StartFromMenu))] public LevelData LoadLevel { get; private set; }
    }
}
