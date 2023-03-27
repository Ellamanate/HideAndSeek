using Cysharp.Threading.Tasks;
using System.Threading;

namespace HideAndSeek
{
    public class StartGame
    {
        private readonly MainGameMediator _mediator;
        private readonly PlayerInput _playerInput;

        public StartGame(MainGameMediator mediator, PlayerInput playerInput)
        {
            _mediator = mediator;
            _playerInput = playerInput;
        }
        
        public async UniTask Start(CancellationToken token)
        {
            _mediator.SetFaderAlpha(1);
            _mediator.SetFaderBlockingRaycasts(true);

            await _mediator.FadeOut(token);

            _playerInput.SetActive(true);
        }
    }
}
