using UnityEngine;

namespace Infrastructure
{
    public class MainGameState : IGameState
    {
        private readonly ScenesConfig _scenesConfig;
        private readonly LoadingScene _loadingScene;

        public MainGameState(ScenesConfig scenesConfig, LoadingScene loadingScene)
        {
            _scenesConfig = scenesConfig;
            _loadingScene = loadingScene;
        }

        public void Enter()
        {
            Time.timeScale = 1;
            _scenesConfig.TryGetGameScene(GameSceneType.GameScene1, out string sceneName);
            _loadingScene.LoadScene(sceneName);
        }
    }
}
