using System;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyVision : ITickable, IDisposable
    {
        private readonly Enemy _enemy;
        private readonly Player _player;
        private readonly GamePause _pause;

        public bool PlayerVisible { get; private set; }

        public EnemyVision(Enemy enemy, Player player, GamePause pause)
        {
            _enemy = enemy;
            _player = player;
            _pause = pause;

            _enemy.OnReseted += Reset;
            _enemy.OnActiveChanged += Reset;
        }
        
        public void Dispose()
        {
            _enemy.OnReseted -= Reset;
            _enemy.OnActiveChanged -= Reset;
        }

        public void Tick()
        {
            if (!_enemy.Model.Destroyed && _enemy.Model.Active && _player.Available && !_pause.Paused)
            {
                Scan();
            }
        }

        private void Scan()
        {
            Vector3 direction = _player.Model.Position - _enemy.Model.Position;

            if (Raycast(out RaycastHit hit) && _player.HittedBody(hit))
            {
                GameLogger.DrawLine(_enemy.Model.Position, hit.point);

                if (!PlayerVisible)
                {
                    PlayerVisible = true;
                    _enemy.UpdateAction();
                }
            }
            else if (PlayerVisible)
            {
                PlayerVisible = false;
                _enemy.UpdateAction();
            }

            bool Raycast(out RaycastHit hit) => Physics.Raycast(_enemy.Model.Position, direction, out hit,
                _enemy.Model.VisionDistance, _enemy.Model.RaycastLayers);
        }

        private void Reset()
        {
            PlayerVisible = false;
        }
    }
}