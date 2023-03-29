using System;
using Cysharp.Threading.Tasks;
using HideAndSeek.Extensions;
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
            HUD = 4
        }

        [SerializeField] private FadeAnimation _fadeAnimation;
        [SerializeField] private FadeAnimation _completeMenu;
        [SerializeField] private FadeAnimation _pauseMenu;
        [SerializeField] private FadeAnimation _hUD;

        private MainGame _mainGame;

        [Inject]
        private void Construct(MainGame mainGame)
        {
            _mainGame = mainGame;
        }

        [Button] public void Exit() => _mainGame.Exit();
        [Button] public void Restart() => _mainGame.RestartGame();
        [Button] public void OpenPauseMenu() => _mainGame.OpenPauseMenu();
        [Button] public void ClosePauseMenu() => _mainGame.ClosePauseMenu();

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
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}