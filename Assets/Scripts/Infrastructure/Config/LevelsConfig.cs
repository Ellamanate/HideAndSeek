using HideAndSeek;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [field: SerializeField] public LevelData[] Levels { get; private set; }
    }
}
