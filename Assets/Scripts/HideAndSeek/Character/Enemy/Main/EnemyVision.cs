using HideAndSeek.AI;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyVision : ITickable
    {
        private readonly EnemyModel _model;
        private readonly EnemyUpdateBody _updateBody;
        private readonly EnemyUpdateBrain _brain;
        private readonly Player _player;
        private readonly GamePause _pause;

        private float _detectionTimer;

        public bool PlayerVisible { get; private set; }

        public EnemyVision(EnemyModel model, EnemyUpdateBody updateBody, EnemyUpdateBrain brain,
            Player player, GamePause pause)
        {
            _model = model;
            _updateBody = updateBody;
            _brain = brain;
            _player = player;
            _pause = pause;
        }

        public void Reinitialize()
        {
            _detectionTimer = 0;
            PlayerVisible = false;
        }

        public void Tick()
        {
            if (!_model.Destroyed && _model.Active && _player.Available && !_pause.Paused)
            {
                if (_player.Model.Visible)
                {
                    Scan();
                }
                else if (PlayerVisible)
                {
                    SetInvisible();
                }
            }
        }

        private void Scan()
        {
            Vector3 direction = _player.UpdateBody.RaycastPosition - _updateBody.RaycastPosition;

            if (DistanceValid() && AngleValid())
            {
                RaycastToPlayer(direction);
            }
            else if (PlayerVisible)
            {
                SetInvisible();
            }

            bool DistanceValid() => direction.magnitude <= _model.VisionDistance;
            bool AngleValid() => Vector3.Angle(_model.SightForward, direction) <= _model.VisionAngle / 2;
        }

        private void RaycastToPlayer(Vector3 direction)
        {
            if (Raycast(out RaycastHit hit) && _player.UpdateBody.HittedBody(hit))
            {
                GameLogger.DrawLine(_updateBody.RaycastPosition, hit.point);

                UpdateTimer(Time.deltaTime);

                if (!PlayerVisible && PlayerDetected())
                {
                    SetVisible();
                }
            }
            else if (PlayerVisible)
            {
                UpdateTimer(-Time.deltaTime);
                SetInvisible();
            }

            bool PlayerDetected() => _detectionTimer + Time.deltaTime > _model.TimeToDetect;
            bool Raycast(out RaycastHit hit) => Physics.Raycast(_updateBody.RaycastPosition, direction, out hit,
                _model.VisionDistance, _model.RaycastLayers);
        }

        private void SetVisible()
        {
            PlayerVisible = true;
            _brain.SetAttentiveness(AttentivenessType.Chase);
            _brain.UpdateAction();
        }

        private void SetInvisible()
        {
            PlayerVisible = false;
            _brain.SetAttentiveness(AttentivenessType.Seaching);
            _brain.UpdateAction();
        }

        private void UpdateTimer(float value)
        {
            _detectionTimer = Mathf.Clamp(_detectionTimer + value, 0, _model.TimeToDetect);
        }
    }
}