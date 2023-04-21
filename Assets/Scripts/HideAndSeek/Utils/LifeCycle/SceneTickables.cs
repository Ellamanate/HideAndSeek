using System.Collections.Generic;
using Zenject;

namespace HideAndSeek.Utils
{
    public class SceneTickables : ITickable, IFixedTickable, ILateTickable
    {
        private List<ITickable> _tickables;
        private List<IFixedTickable> _fixedTickables;
        private List<ILateTickable> _lateTickables;

        public SceneTickables()
        {
            _tickables = new List<ITickable>();
            _fixedTickables = new List<IFixedTickable>();
            _lateTickables = new List<ILateTickable>();
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

        public void LateTick()
        {
            for (int i = 0; i < _lateTickables.Count; i++)
            {
                _lateTickables[i].LateTick();
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

        public void AddLateTickable(ILateTickable tickable)
        {
            if (!_lateTickables.Contains(tickable))
            {
                _lateTickables.Add(tickable);
            }
        }

        public void RemoveLateTickable(ILateTickable tickable)
        {
            _lateTickables.Remove(tickable);
        }
    }
}