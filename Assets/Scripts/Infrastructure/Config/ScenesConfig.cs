using Tymski;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/ScenesConfig", fileName = "ScenesConfig")]
    public class ScenesConfig : ScriptableObject
    {
        [field: SerializeField] public SceneReference MainMenu { get; private set; }
    }
}
