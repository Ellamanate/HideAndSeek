using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<string, IGameState> _states;

        private IGameState _currentState;

        public GameStateMachine(BootstrapState.Factory bootstrapFactory, MainGameState mainGameState, MenuState menuState)
        {
            _states = new Dictionary<string, IGameState>
            {
                { nameof(BootstrapState), bootstrapFactory.Create(this) },
                { nameof(MainGameState), mainGameState },
                { nameof(MenuState), menuState }
            };
        }

        public void MoveToState<T>() where T : IGameState
        {
            string name = typeof(T).Name;

            if (_states.TryGetValue(name, out IGameState state))
            {
                _currentState = state;
                state.Enter();
            }
            else
            {
                throw new Exception($"Cannot get state by type {name}");
            }
        }
    }
}
