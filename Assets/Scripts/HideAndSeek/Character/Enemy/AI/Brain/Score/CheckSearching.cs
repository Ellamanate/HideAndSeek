﻿using Zenject;

namespace HideAndSeek.AI
{
    public class CheckSearching : IScoreCounter
    {
        private readonly Enemy _enemy;
        private readonly EnemyPatrol _patrol;
        private readonly SceneInteractions _sceneInteractions;
        private readonly Actions<OrderActionType> _actions;

        public CheckSearching(Actions<OrderActionType> actions, Enemy enemy, EnemyPatrol patrol,
            SceneInteractions sceneInteractions)
        {
            _actions = actions;
            _enemy = enemy;
            _patrol = patrol;
            _sceneInteractions = sceneInteractions;
        }

        public void CalculateScore()
        {
            bool searchingState = SearchingState();

            if (_enemy.Model.Moved && searchingState && DestinationNotPatrol())
            {
                _actions.AddScoreTo(OrderActionType.Search, 0.75f);
            }

            if (_enemy.Model.PlayerDetectedInShelter)
            {
                _actions.AddScoreTo(OrderActionType.MoveToInteraction, 1);
            }
            else if (searchingState && _sceneInteractions.TryGetPriorityInteractionNear(_enemy, out var interactable))
            {
                _actions.AddScoreTo(OrderActionType.MoveToInteraction, 0.5f);
            }

            bool SearchingState() => _enemy.Model.CurrentAttentiveness == AttentivenessType.Seaching;
            bool DestinationNotPatrol() => !_patrol.IsPatrolPoint(_enemy.Model.Destination);
        }

        public class Factory : PlaceholderFactory<CheckSearching> { }
    }
}