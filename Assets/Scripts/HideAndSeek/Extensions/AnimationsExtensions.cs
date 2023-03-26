using Cysharp.Threading.Tasks;
using System.Threading;

namespace HideAndSeek.Extensions
{
    public static class AnimationsExtension
    {
        public static async UniTask FadeIn(this FadeAnimation fade, CancellationToken token = default)
        {
            fade.SetBlockingRaycasts(true);
            fade.SetTargetFade(1);

            await fade.Play(token);
        }

        public static async UniTask FadeOut(this FadeAnimation fade, CancellationToken token = default)
        {
            fade.SetTargetFade(0);

            await fade.Play(token);

            fade.SetBlockingRaycasts(false);
        }
    }
}
