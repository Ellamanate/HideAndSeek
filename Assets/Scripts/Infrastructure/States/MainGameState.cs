using HideAndSeek;
using UnityEngine;

namespace Infrastructure
{
    public class MainGameState : IGameState
    {
        private readonly LoadingScene _loadingScene;
        private readonly LevelsService _levels;

        public MainGameState(LoadingScene loadingScene, LevelsService levels)
        {
            _loadingScene = loadingScene;
            _levels = levels;
        }

        public void Enter()
        {
            Time.timeScale = 1;
            _loadingScene.LoadScene(_levels.CurrentLevel.Scene);
        }
    }
}
