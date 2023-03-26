using Cysharp.Threading.Tasks;

namespace HideAndSeek
{
    public class StartGame
    {
        private readonly MainGameMediator _mediator;
        private readonly Player _player;
        private readonly PlayerInput _input;
        private readonly UpdateGame _updateGame;

        public StartGame(MainGameMediator mediator, Player player, PlayerInput input, UpdateGame updateGame)
        {
            _mediator = mediator;
            _player = player;
            _input = input;
            _updateGame = updateGame;
        }

        public void Start()
        {
            _updateGame.AddFixedTickable(_input);

            _mediator.SetFaderAlpha(1);
            _mediator.SetFaderBlockingRaycasts(true);

            _ = PlayStartAnimation();
        }

        private async UniTask PlayStartAnimation()
        {
            await _mediator.FadeOut();

            _player.SetActive(true);
        }
    }
}
