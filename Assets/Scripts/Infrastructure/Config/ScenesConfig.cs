using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(menuName = "Configs/ScenesConfig", fileName = "ScenesConfig")]
    public class ScenesConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<GameSceneType, string> _gameScenes;

        [OdinSerialize] public string MenuSceneName { get; private set; }

        public bool TryGetGameScene(GameSceneType type, out string sceneName) 
            => _gameScenes.TryGetValue(type, out sceneName);
    }
}
