using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;

namespace HideAndSeek
{
    public class FadeAnimation : MonoBehaviour, IAnimatable
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField, MinValue(0)] private float _duration;
        [SerializeField, MinValue(0), MaxValue(1)] private float _targetFade;

        private Tween _tween;

        private void OnDestroy()
        {
            _tween?.Kill();
        }

        public void SetAlpha(float alpha)
        {
            _canvasGroup.alpha = alpha;

            if (_tween != null && _tween.active)
            {
                GameLogger.LogWarning("Setting alpha when tween runing");
            }
        }

        public void SetTargetFade(float targetFade)
        {
            _targetFade = targetFade;
        }

        public void SetBlockingRaycasts(bool blocksRaycasts)
        {
            _canvasGroup.blocksRaycasts = blocksRaycasts;
        }

        public async UniTask Play(CancellationToken token = default)
        {
            if (_tween != null && _tween.active)
            {
                _tween.Kill();
            }

            _tween = DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, _targetFade, _duration);
            await _tween.AsyncWaitForKill(token);
        }
    }
}