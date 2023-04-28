using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerHUDMediator : MonoBehaviour
    {
        [SerializeField] private FadeAnimation _interact;

        private PlayerHUD _hud;

        private void Construct(PlayerHUD hud)
        {
            _hud = hud;
        }

        public void SetInteractEnable(float alpha, bool blocksRaycasts)
        {
            _interact.SetAlpha(alpha);
            _interact.SetBlockingRaycasts(blocksRaycasts);
        }

        public async UniTask ShowInteract(CancellationToken token)
        {
            await _interact.FadeIn(token);
        }        
        
        public async UniTask HideInteract(CancellationToken token)
        {
            await _interact.FadeOut(token);
        }
    }
}