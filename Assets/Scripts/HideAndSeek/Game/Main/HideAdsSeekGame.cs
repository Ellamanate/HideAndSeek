using Zenject;

namespace HideAndSeek
{
    public class HideAdsSeekGame
    {
        private readonly Player _player;
        private readonly StartGame _startGame;

        public HideAdsSeekGame(Player player, StartGame startGame)
        {
            _player = player;
            _startGame = startGame;
        }

        public void Start() => _startGame.Start();

        public class Factory : PlaceholderFactory<Player, HideAdsSeekGame> { }
    }
}
