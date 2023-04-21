using Zenject;

namespace HideAndSeek.Utils
{
    public class SubContainerTickables : ITickable, IFixedTickable, ILateTickable
    {
        private ITickable[] _tickables;
        private IFixedTickable[] _fixedTickables;
        private ILateTickable[] _lateTickables;

        public SubContainerTickables([Inject(Source = InjectSources.Local)] ITickable[] tickables, 
            [Inject(Source = InjectSources.Local)] IFixedTickable[] fixedTickables,
            [Inject(Source = InjectSources.Local)] ILateTickable[] lateTickables)
        {
            _tickables = tickables;
            _fixedTickables = fixedTickables;
            _lateTickables = lateTickables;
        }

        public void Tick()
        {
            for (int i = 0; i < _tickables.Length; i++)
            {
                _tickables[i].Tick();
            }
        }

        public void FixedTick()
        {
            for (int i = 0; i < _fixedTickables.Length; i++)
            {
                _fixedTickables[i].FixedTick();
            }
        }

        public void LateTick()
        {
            for (int i = 0; i < _lateTickables.Length; i++)
            {
                _lateTickables[i].LateTick();
            }
        }
    }
}