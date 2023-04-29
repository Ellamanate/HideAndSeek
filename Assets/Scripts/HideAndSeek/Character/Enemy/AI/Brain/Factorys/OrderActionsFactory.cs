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
        private readonly Interact.Factory _interactFactory;
        private readonly MoveToInteraction.Factory _moveToInteractionFactory;

        public OrderActionsFactory(Idle.Factory idleFactory, Chase.Factory chaseFactory, Search.Factory searchFactory,
            Patrol.Factory patrolFactory, Interact.Factory interactFactory, MoveToInteraction.Factory moveToInteractionFactory)
        {
            _idleFactory = idleFactory;
            _chaseFactory = chaseFactory;
            _searchFactory = searchFactory;
            _patrolFactory = patrolFactory;
            _interactFactory = interactFactory;
            _moveToInteractionFactory = moveToInteractionFactory;
        }

        public Dictionary<OrderActionType, IAction> Create(OrderActionType types)
        {
            Dictionary<OrderActionType, IAction> actions = new Dictionary<OrderActionType, IAction>();

            Add(OrderActionType.Idle, _idleFactory);
            Add(OrderActionType.Chase, _chaseFactory);
            Add(OrderActionType.Search, _searchFactory);
            Add(OrderActionType.Patrol, _patrolFactory);
            Add(OrderActionType.Interact, _interactFactory);
            Add(OrderActionType.MoveToInteraction, _moveToInteractionFactory);

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
