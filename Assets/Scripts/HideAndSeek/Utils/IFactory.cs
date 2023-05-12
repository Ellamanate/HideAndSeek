namespace HideAndSeek
{
    public interface IFactory<TCreate, TArg>
    {
        public TCreate Create(TArg arg);
    }
}
