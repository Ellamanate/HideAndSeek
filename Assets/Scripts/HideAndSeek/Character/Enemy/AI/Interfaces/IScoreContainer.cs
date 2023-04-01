namespace HideAndSeek.AI
{
    public interface IScoreContainer
    {
        public float Score { get; }
        public void AddScore(float score);
        public void Disable();
        public void ToDefault();
    }
}
