using System.Collections.Generic;
using Zenject;

namespace HideAndSeek.AI
{
    public class OrderActionsFactory : IActionsFactory<OrderActionType>
    {
        private readonly Idle.Factory _idleFactory;
        private readonly Chase.Factory _chaseFactory;
        private readonly Search.Factory _searchFactory;
        private readonly Patrol.Factory _patrolFactory;

        public OrderActionsFactory(Idle.Factory idleFactory, Chase.Factory chaseFactory, Search.Factory searchFactory,
            Patrol.Factory patrolFactory)
        {
            _idleFactory = idleFactory;
            _chaseFactory = chaseFactory;
            _searchFactory = searchFactory;
            _patrolFactory = patrolFactory;
        }

        public Dictionary<OrderActionType, IAction> Create(OrderActionType types)
        {
            Dictionary<OrderActionType, IAction> actions = new Dictionary<OrderActionType, IAction>();

            Add(OrderActionType.Idle, _idleFactory);
            Add(OrderActionType.Chase, _chaseFactory);
            Add(OrderActionType.Search, _searchFactory);
            Add(OrderActionType.Patrol, _patrolFactory);

            return actions;

            void Add<T>(OrderActionType targetType, IFactory<T> factory) where T : IAction
            {
                if (types.HasFlag(targetType))
                {
                    IAction action = factory.Create();
                    action.ToDefault();
                    actions.Add(targetType, action);
                }
            }
        }
    }
}
