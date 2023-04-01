using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyVision : ITickable
    {
        private readonly Enemy _enemy;
        private readonly Player _player;

        public bool PlayerVisible { get; private set; }

        public EnemyVision(Enemy enemy, Player player)
        {
            _enemy = enemy;
            _player = player;
        }

        private bool Available => !_enemy.Model.Destroyed;

        public void Tick()
        {
            if (Available && _player.Available)
            {
                Vector3 direction = _player.Model.Position - _enemy.Model.Position;
                
                if (!PlayerVisible && Raycast(out RaycastHit hit) && _player.HittedBody(hit))
                {
                    GameLogger.DrawLine(_enemy.Model.Position, hit.point);
                    PlayerVisible = true;
                    _enemy.UpdateAction();
                }
                else if (PlayerVisible)
                {
                    PlayerVisible = false;
                    _enemy.UpdateAction();
                }

                bool Raycast(out RaycastHit hit) => Physics.Raycast(_enemy.Model.Position, direction, out hit,
                    _enemy.Model.VisionDistance, _enemy.Model.RaycastLayers);
            }
        }
    }
}