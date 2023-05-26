using System;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class MainGameMediator : SerializedMonoBehaviour
    {
        public enum FadeType
        {
            Screen = 1,
            Complete = 2,
            Pause = 3,
            HUD = 4,
            Fail = 5
        }

        [SerializeField] private FadeAnimation _fadeAnimation;
        [SerializeField] private FadeAnimation _completeMenu;
        [SerializeField] private FadeAnimation _pauseMenu;
        [SerializeField] private FadeAnimation _hUD;
        [SerializeField] private FadeAnimation _fail;

        private MainGame _mainGame;
        private RestartGame _restart;
        private GameMenu _gameMenu;

        [Inject]
        private void Construct(MainGame mainGame, RestartGame restart, GameMenu gameMenu)
        {
            _mainGame = mainGame;
            _restart = restart;
            _gameMenu = gameMenu;
        }

        [Button] public void Exit() => _mainGame.Exit();
        [Button] public void Restart() => _restart.Restart();
        [Button] public void LoadLevel() => _mainGame.LoadLevel();
        [Button] public void OpenPauseMenu() => _gameMenu.OpenPauseMenu();
        [Button] public void ClosePauseMenu() => _gameMenu.ClosePauseMenu();

        public void SetAlpha(FadeType type, float alpha) => GetFader(type).SetAlpha(alpha);
        public void SetBlockingRaycasts(FadeType type, bool blocksRaycasts) => GetFader(type).SetBlockingRaycasts(blocksRaycasts);
        
        public async UniTask FadeIn(FadeType type, CancellationToken token = default) => await GetFader(type).FadeIn(token);
        public async UniTask FadeOut(FadeType type, CancellationToken token = default) => await GetFader(type).FadeOut(token);

        private FadeAnimation GetFader(FadeType type)
        {
            return type switch
            {
                FadeType.Screen => _fadeAnimation,
                FadeType.Complete => _completeMenu,
                FadeType.Pause => _pauseMenu,
                FadeType.HUD => _hUD,
                FadeType.Fail => _fail,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}