namespace HideAndSeek
{
    public class StartGame
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly MainGameMediator _mediator;

        private Player _player;

        public StartGame(PlayerSpawner playerSpawner, MainGameMediator mediator)
        {
            _playerSpawner = playerSpawner;
            _mediator = mediator;
        }

        public void Start()
        {
            _player = _playerSpawner.Spawn();
            _player.SetActive(true);
            _player.SetUpdating(true);

            _mediator.SetFaderAlpha(1);
            _mediator.SetFaderBlockingRaycasts(true);

            _ = _mediator.FadeOut();
        }
    }
}
