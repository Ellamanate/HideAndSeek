using UnityEngine;

namespace Infrastructure
{
    public class MainGameState : IGameState
    {
        private readonly BootstrapConfig _bootstrapConfig;
        private readonly ScenesConfig _scenesConfig;
        private readonly LoadingScene _loadingScene;

        public MainGameState(BootstrapConfig bootstrapConfig, ScenesConfig scenesConfig, LoadingScene loadingScene)
        {
            _bootstrapConfig = bootstrapConfig;
            _scenesConfig = scenesConfig;
            _loadingScene = loadingScene;
        }

        public void Enter()
        {
            Time.timeScale = 1;
            _scenesConfig.TryGetGameScene(_bootstrapConfig.StartScene, out string sceneName);
            _loadingScene.LoadScene(sceneName);
        }
    }
}
