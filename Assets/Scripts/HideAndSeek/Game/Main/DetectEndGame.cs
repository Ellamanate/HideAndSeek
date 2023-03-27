using System;

namespace HideAndSeek
{
    public class DetectEndGame : IDisposable
    {
        private readonly MainGame _game;
        private readonly Player _player;

        public DetectEndGame(MainGame game, Player player)
        {
            _game = game;
            _player = player;

            _player.OnTriggerEnter += CheckFinish;
        }

        public void Dispose()
        {
            _player.OnTriggerEnter -= CheckFinish;
        }

        private void CheckFinish(ITrigger trigger)
        {
            if (trigger is GoalTrigger)
            {
                _game.CompleteGame();
            }
        }
    }
}
