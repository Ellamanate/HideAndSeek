using System;
using System.Collections.Generic;
using HideAndSeek.Utils;

namespace HideAndSeek.AI
{
    public class Actions<T> where T : Enum
    {
        private readonly IActionsFactory<T> _factory;

        private Dictionary<T, IAction> _actions;

        public Actions(IActionsFactory<T> factory)
        {
            _factory = factory;
        }

        public void Initialize(T types)
        {
            _actions = _factory.Create(types);
        }

        public void ExecuteAction(out T type)
        {
            if (_actions.Count > 0)
            {
                var actionPair = _actions.MaxBy(x => x.Value.Score);
                GameLogger.Log($"Current {typeof(T).Name} action: {actionPair.Key} Score: {actionPair.Value.Score}");
                actionPair.Value.Execute();
                type = actionPair.Key;

                return;
            }

            type = default;
        }

        public void AddScoreTo(T type, float score)
        {
            if (_actions.TryGetValue(type, out IAction action))
            {
                action.AddScore(score);
            }
        }

        public void Disable(T type)
        {
            if (_actions.TryGetValue(type, out IAction action))
            {
                action.Disable();
            }
        }

        public void ToDefault()
        {
            foreach (IAction action in _actions.Values)
            {
                action.ToDefault();
            }
        }
    }
}
