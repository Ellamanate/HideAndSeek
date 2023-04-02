using System;
using UnityEngine;

namespace HideAndSeek
{
    public class GamePause
    {
        public event Action OnPauseChanged;
        
        private bool _paused;

        public GamePause()
        {
            
        }

        public bool Paused
        {
            get => _paused;
            private set
            {
                _paused = value;
                OnPauseChanged?.Invoke();
            }
        }

        public void Pause()
        {
            Paused = true;
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Paused = false;
            Time.timeScale = 1;
        }
    }
}
