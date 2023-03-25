using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/BootstrapConfig", fileName = "BootstrapConfig")]
    public class BootstrapConfig : SerializedScriptableObject
    {
        [OdinSerialize] public bool StartFromMenu { get; private set; }
        [OdinSerialize, HideIf(nameof(StartFromMenu))] public GameSceneType StartScene { get; private set; }
    }
}
