using System.Threading;
using Cysharp.Threading.Tasks;

namespace HideAndSeek
{
    public class GameOver
    {
        private readonly MainGameMediator _mediator;
        private readonly PlayerInput _playerInput;

        public GameOver(MainGameMediator mediator, PlayerInput playerInput)
        {
            _mediator = mediator;
            _playerInput = playerInput;
        }

        public async UniTask FailGame(CancellationToken token)
        {
            _playerInput.SetActive(false);
            await _mediator.FadeIn(token);
        }

        public async UniTask CompleteGame(CancellationToken token)
        {
            _playerInput.SetActive(false);
            await _mediator.FadeIn(token);
        }
    }
}
