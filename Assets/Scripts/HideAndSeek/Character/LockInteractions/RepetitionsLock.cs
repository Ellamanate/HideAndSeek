namespace HideAndSeek
{
    public class RepetitionsLock
    {
        private int _repetitionsToLock;
        private int _currentRepetitions;

        public RepetitionsLock(int repetitionsToLock)
        {
            _repetitionsToLock = repetitionsToLock;
            _currentRepetitions = 0;
        }

        public bool Locked => _currentRepetitions >= _repetitionsToLock;

        public void Increment()
        {
            _currentRepetitions++;
        }

        public void SetMaxRepetitions(int repetitionsToLock)
        {
            _repetitionsToLock = repetitionsToLock;
        }

        public void DropCounter()
        {
            _currentRepetitions = 0;
        }
    }
}
