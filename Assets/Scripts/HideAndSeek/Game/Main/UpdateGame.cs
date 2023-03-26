using System.Collections.Generic;
using Zenject;

namespace HideAndSeek
{
    public class UpdateGame
    {
        private List<ITickable> _tickables;
        private List<IFixedTickable> _fixedTickables;

        public UpdateGame()
        {
            _tickables = new List<ITickable>();
            _fixedTickables = new List<IFixedTickable>();
        }

        public void Tick()
        {
            for (int i = 0; i < _tickables.Count; i++)
            {
                _tickables[i].Tick();
            }
        }

        public void FixedTick()
        {
            for (int i = 0; i < _fixedTickables.Count; i++)
            {
                _fixedTickables[i].FixedTick();
            }
        }

        public void AddTickable(ITickable tickable)
        {
            if (!_tickables.Contains(tickable))
            {
                _tickables.Add(tickable);
            }
        }

        public void RemoveTickable(ITickable tickable)
        {
            _tickables.Remove(tickable);
        }

        public void AddFixedTickable(IFixedTickable tickable)
        {
            if (!_fixedTickables.Contains(tickable))
            {
                _fixedTickables.Add(tickable);
            }
        }

        public void RemoveFixedTickable(IFixedTickable tickable)
        {
            _fixedTickables.Remove(tickable);
        }
    }
}
