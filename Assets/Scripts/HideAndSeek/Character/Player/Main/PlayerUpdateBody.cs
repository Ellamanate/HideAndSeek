using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PlayerUpdateBody : ITransformable, ITickable
    {
        private readonly PlayerModel _model;

        private PlayerBody _body;

        public Vector3 RaycastPosition => _body.RaycastPosition.position;
        public Vector3 Position => _model.Position;
        public Quaternion Rotation => _model.Rotation;

        public PlayerUpdateBody(PlayerModel model)
        {
            _model = model;
        }

        public bool Available => !_model.Destroyed && _body != null;
        public Transform Transform => _body.transform;

        public void Initialize(PlayerBody body)
        {
            _body = body;
        }

        public void Tick()
        {
            if (Available)
            {
                _model.Position = _body.transform.position;
                _model.Rotation = _body.transform.rotation;
            }
        }

        public bool HittedBody(RaycastHit hit)
        {
            return Available && _body.HittedBody(hit);
        }

        public void SetVelocity(Vector3 velocity)
        {
            if (Available)
            {
                _body.Movement.SetVelocity(velocity);
            }
        }

        public void SetSpeed(float speed)
        {
            if (Available)
            {
                _body.Movement.SetSpeed(speed);
                _model.Speed = speed;
            }
        }

        public void SetPosition(Vector3 position)
        {
            if (Available)
            {
                _body.Movement.SetPosition(position);
                _model.Position = position;
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            if (Available)
            {
                _body.Movement.SetRotation(rotation);
                _model.Rotation = rotation;
            }
        }
    }
}