using UnityEngine;

namespace HideAndSeek.AI
{
    public abstract class BaseAction : IAction
    {
        private bool _enabled;

        public float Score { get; private set; }

        public void AddScore(float score)
        {
            if (_enabled)
            {
                Score += Mathf.Clamp(score, 0, 1);
            }
        }

        public void Disable()
        {
            Score = 0;
            _enabled = false;
        }

        public void ToDefault()
        {
            Score = 0;
            _enabled = true;
        }

        public abstract void Execute();
    }
}
