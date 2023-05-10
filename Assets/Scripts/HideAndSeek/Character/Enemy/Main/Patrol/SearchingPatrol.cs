using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class SearchingPatrol : BasePatrol
    {
        private readonly PlayerModel _playerModel;
        private readonly SearchingPoint[] _searchingPoints;

        public SearchingPoint CurrentPoint { get; private set; }

        public SearchingPatrol(PlayerModel playerModel, EnemyModel model, EnemyBody body, 
            SearchingPoint[] searchingPoints) : base(model, body)
        {
            _playerModel = playerModel;
            _searchingPoints = searchingPoints;
        }

        public SearchingPoint GetNearestSearchingPoint()
        {
            return _searchingPoints.GetNearest(x => x.Position, _playerModel.Position);
        }

        public void SetNearestSearchingPoint(SearchingPoint point)
        {
            CurrentPoint = point;
            PatrolPoints = CurrentPoint.GetPatrolPoints();
        }

        public override void SetNextPoint()
        {
            CurrentPatrolIndex = Utilities.RandomExceptValues(0, PatrolPoints.Length - 1, CurrentPatrolIndex);
        }
    }
}
