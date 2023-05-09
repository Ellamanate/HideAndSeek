using HideAndSeek.Utils;
using System.Linq;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class SearchingPoint : MonoBehaviour
    {
        [SerializeField] private PatrolPoint[] _patrolPoints;
        [SerializeField] private Shelter[] _shelters;

        private HidePlayer _hidePlayer;

        public Vector3 Position => transform.position;

        [Inject]
        private void Construct(HidePlayer hidePlayer)
        {
            _hidePlayer = hidePlayer;
        }

        public PatrolPoint[] GetPatrolPoints()
        {
            return _patrolPoints.ToArray();
        }

        public Shelter GetRandomShelter()
        {
            if (_shelters.Length > 0)
            {
                return _shelters.GetRandom();
            }
            else
            {
                return null;
            }
        }

        public Shelter GetRandomEmptyShelter()
        {
            if (_hidePlayer.HasShelter)
            {
                return _shelters
                    .Where(x => _hidePlayer.CurrentShelter != x)
                    .GetRandom();
            }
            else
            {
                return _shelters.GetRandom();
            }
        }

        public Shelter GetPlayerShelter()
        {
            if (_hidePlayer.HasShelter)
            {
                return _shelters.FirstOrDefault(x => _hidePlayer.CurrentShelter == x);
            }

            return null;
        }
    }
}