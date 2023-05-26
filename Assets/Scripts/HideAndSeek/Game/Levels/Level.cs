using Tymski;

namespace HideAndSeek
{
    public class Level
    {
        private readonly LevelData _data;

        public bool Opened { get; set; }
        public bool Completed { get; set; }
        public SceneReference Scene => _data.Scene;

        public Level(LevelData data)
        {
            _data = data;
        }

        public void Complete()
        {
            Completed = true;
        }

        public void Open()
        {
            Opened = true;
        }
    }
}
