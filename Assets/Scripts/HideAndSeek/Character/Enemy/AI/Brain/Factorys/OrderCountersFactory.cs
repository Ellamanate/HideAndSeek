using System.Collections.Generic;
using Zenject;

namespace HideAndSeek.AI
{
    public class OrderCountersFactory : ICountersFactory<OrderCounterType>
    {
        private readonly CheckVisible.Factory _visibleFactory;
        private readonly CheckSleep.Factory _sleepFactory;
        private readonly CheckSearching.Factory _searchingFactory;

        public OrderCountersFactory(CheckVisible.Factory visibleFactory, CheckSleep.Factory sleepFactory,
            CheckSearching.Factory searchingFactory)
        {
            _visibleFactory = visibleFactory;
            _sleepFactory = sleepFactory;
            _searchingFactory = searchingFactory;
        }

        public List<IScoreCounter> Create(OrderCounterType controllerTypes)
        {
            List<IScoreCounter> counters = new List<IScoreCounter>();

            Add(OrderCounterType.CheckVisible, _visibleFactory);
            Add(OrderCounterType.CheckSleep, _sleepFactory);
            Add(OrderCounterType.CheckSearching, _searchingFactory);

            return counters;

            void Add<T>(OrderCounterType type, IFactory<T> factory) where T : IScoreCounter
            {
                if (controllerTypes.HasFlag(type))
                {
                    counters.Add(factory.Create());
                }
            }
        }
    }    
}
