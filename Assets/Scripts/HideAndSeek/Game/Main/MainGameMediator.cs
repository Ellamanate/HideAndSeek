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
        [SerializeField] FadeAnimation _fadeAnimation;

        private MainGame _mainGame;

        [Inject]
        private void Construct(MainGame mainGame)
        {
            _mainGame = mainGame;
        }

        [Button] public void Exit() => _mainGame.Exit();

        public void SetFaderAlpha(float alpha) => _fadeAnimation.SetAlpha(alpha);
        public void SetFaderBlockingRaycasts(bool blocksRaycasts) => _fadeAnimation.SetBlockingRaycasts(blocksRaycasts);
        public async UniTask FadeIn(CancellationToken token = default) => await _fadeAnimation.FadeIn(token);
        public async UniTask FadeOut(CancellationToken token = default) => await _fadeAnimation.FadeOut(token);
    }
}