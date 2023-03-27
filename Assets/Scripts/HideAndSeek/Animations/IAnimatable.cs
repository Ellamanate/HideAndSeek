using Cysharp.Threading.Tasks;
using System.Threading;

namespace HideAndSeek
{
    public interface IAnimatable
    {
        public UniTask Play(CancellationToken token = default);
    }
}
