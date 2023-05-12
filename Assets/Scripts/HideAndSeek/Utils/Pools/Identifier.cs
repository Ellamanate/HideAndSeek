namespace HideAndSeek
{
    public struct Identifier<TObject, TId>
    {
        private TObject _object;
        private TId _id;

        public TObject Object => _object;
        public TId Id => _id;

        public Identifier(TObject obj, TId id)
        {
            _object = obj;
            _id = id;
        }
    }
}
