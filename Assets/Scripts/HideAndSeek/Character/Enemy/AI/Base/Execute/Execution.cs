using System;
using System.Collections.Generic;

namespace HideAndSeek.AI
{
    public class Execution<TAction, TScore> where TAction : Enum where TScore : Enum
    {
        private readonly Actions<TAction> _actions;
        private readonly ICountersFactory<TScore> _countersFactory;

        private List<IScoreCounter> _counters;

        public Execution(Actions<TAction> actions, ICountersFactory<TScore> countersFactory)
        {
            _actions = actions;
            _countersFactory = countersFactory;
        }

        public void Initialize(TScore types)
        {
            _counters = _countersFactory.Create(types);
        }

        public void Execute()
        {
            _actions.ToDefault();

            foreach (var counter in _counters)
            {
                counter.CalculateScore();
            }

            _actions.ExecuteAction(out TAction type);

            OnActionExecuted(type);
        }

        protected virtual void OnActionExecuted(TAction type) { }
    }
}
