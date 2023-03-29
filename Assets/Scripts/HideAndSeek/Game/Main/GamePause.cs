using UnityEngine;

namespace HideAndSeek
{
    public class GamePause
    {
        public GamePause()
        {
            
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
        }
    }
}
