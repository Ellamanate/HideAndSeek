using Zenject;

namespace HideAndSeek.Utils
{
    public class SubContainerTickables : ITickable, IFixedTickable
    {
        private ITickable[] _tickables;
        private IFixedTickable[] _fixedTickables;

        public SubContainerTickables([Inject(Source = InjectSources.Local)] ITickable[] tickables, 
            [Inject(Source = InjectSources.Parent)] IFixedTickable[] fixedTickables)
        {
            _tickables = tickables;
            _fixedTickables = fixedTickables;
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
    }
}