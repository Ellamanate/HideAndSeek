using System.Collections.Generic;

namespace HideAndSeek
{
    public class GlobalSearching
    {
        private readonly Dictionary<SearchingPoint, EnemyPatrol> _busyPoints;

        public GlobalSearching()
        {
            _busyPoints = new Dictionary<SearchingPoint, EnemyPatrol>();
        }

        public void Clear()
        {
            _busyPoints.Clear();
        }

        public bool TryGetPoint(SearchingPoint point, out EnemyPatrol enemy)
        {
            return _busyPoints.TryGetValue(point, out enemy);
        }

        public void TakePoint(EnemyPatrol enemy, SearchingPoint point)
        {
            _busyPoints[point] = enemy;
        }

        public void ClearPoint(SearchingPoint point)
        {
            _busyPoints.Remove(point);
        }
    }
}
