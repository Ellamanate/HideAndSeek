using UnityEngine;
using Zenject;

namespace HideAndSeek.AI
{
    public class Search : BaseAction
    {
        private readonly EnemyModel _model;
        private readonly EnemyMovement _movement;
        private readonly EnemyPatrol _patrol;

        public Search(EnemyModel model, EnemyMovement movement, EnemyPatrol patrol)
        {
            _model = model;
            _movement = movement;
            _patrol = patrol;
        }

        public override void Execute()
        {
            _movement.StopChase(_model.StopChaseDelay);
            _patrol.SetSearchingState();

            if (!_model.Moved)
            {
                Vector3 patrolPointPosition = _patrol.GetCurrentPatrolPosition();
                _movement.MoveTo(patrolPointPosition);
            }
        }

        public class Factory : PlaceholderFactory<Search> { }
    }
}
