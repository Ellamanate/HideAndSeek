using Infrastructure;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyVision : ITickable
    {
        private readonly EnemyModel _model;
        private readonly Player _player;

        public EnemyVision(EnemyModel model, Player player)
        {
            _model = model;
            _player = player;
        }

        private bool Available => !_model.Destroyed;

        public void Tick()
        {
            if (Available && _player.Available)
            {
                Vector3 direction = _player.Model.Position - _model.Position;
                
                if (Physics.Raycast(_model.Position, direction, out RaycastHit hit, _model.VisionDistance, _model.RaycastLayers) 
                    && _player.HittedBody(hit))
                {
                    GameLogger.DrawLine(_model.Position, hit.point);
                    GameLogger.Log(hit.collider.name);
                }
            }
        }
    }
}