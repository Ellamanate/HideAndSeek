using UnityEngine;

namespace Infrastructure
{
    public class MenuState : IGameState
    {
        private readonly ScenesConfig _scenesConfig;
        private readonly LoadingScene _loadingScene;

        public MenuState(ScenesConfig scenesConfig, LoadingScene loadingScene)
        {
            _scenesConfig = scenesConfig;
            _loadingScene = loadingScene;
        }

        public void Enter()
        {
            Time.timeScale = 1;
            _loadingScene.LoadScene(_scenesConfig.MainMenu);
        }
    }
}
