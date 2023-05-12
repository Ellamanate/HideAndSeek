using Cysharp.Threading.Tasks;
using System.Threading;

namespace HideAndSeek
{
    public interface IAnimatableInteraction<T>
    {
        public UniTask PlayInteractionAnimation(T interactor, CancellationToken token);
    }
}
