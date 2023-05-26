using Tymski;
using UnityEngine;

namespace HideAndSeek
{
    [CreateAssetMenu(menuName = "Levels/LevelData", fileName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public SceneReference Scene { get; private set; }
    }
}
