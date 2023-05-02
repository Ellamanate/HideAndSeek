namespace HideAndSeek
{
    public class FailGame
    {
        private readonly SetGameState _gameState;

        public FailGame(SetGameState gameState)
        {
            _gameState = gameState;
        }

        public void SetFail()
        {
            _gameState.FailGame();
        }
    }
}
