using System.Collections.Generic;

namespace HideAndSeek.AI
{
    public class OrderCountersFactory : ICountersFactory<OrderCounterType>
    {
        private readonly CheckVisible.Factory _visibleFactory;

        public OrderCountersFactory(CheckVisible.Factory visibleFactory)
        {
            _visibleFactory = visibleFactory;
        }

        public List<IScoreCounter> Create(OrderCounterType controllerTypes)
        {
            List<IScoreCounter> counters = new List<IScoreCounter>();

            Add(OrderCounterType.CheckVisible);

            return counters;

            void Add(OrderCounterType type)
            {
                if (controllerTypes.HasFlag(type))
                {
                    counters.Add(_visibleFactory.Create());
                }
            }
        }
    }    
}
