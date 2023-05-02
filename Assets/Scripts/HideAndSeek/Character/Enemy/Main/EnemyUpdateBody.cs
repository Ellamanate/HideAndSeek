using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemyUpdateBody : ITransformable, ITickable
    {
        public readonly EnemyModel Model;

        private readonly EnemyBody _body;
        private readonly GamePause _pause;

        public EnemyUpdateBody(EnemyModel model, EnemyBody body, GamePause pause)
        {
            Model = model;
            _body = body;
            _pause = pause;
        }

        public Vector3 RaycastPosition => _body.RaycastPosition.position;
        public Vector3 Position => Model.Position;
        public Quaternion Rotation => Model.Rotation;

        public void Reinitialize()
        {
            _body.Movement.SetMaxSpeed(Model.Speed);
            _body.VisionCone.SetConeData(Model.ConeData);
            _body.VisionCone.Initialize();

            SetPosition(Model.Position);
            SetRotation(Model.Rotation);
        }

        public void Tick()
        {
            if (!Model.Destroyed && !_pause.Paused)
            {
                Model.Position = _body.transform.position;
                Model.Rotation = _body.transform.rotation;
                Model.SightRotation = _body.SightRotation;
            }
        }

        public void SetPosition(Vector3 position)
        {
            if (!Model.Destroyed && Model.Active)
            {
                _body.Movement.Warp(position);
                Model.Position = position;
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            if (!Model.Destroyed && Model.Active)
            {
                _body.Movement.SetRotation(rotation);
                Model.Rotation = rotation;
            }
        }

        public void SetViewRotation(Quaternion rotation)
        {
            if (!Model.Destroyed && Model.Active)
            {
                _body.SetViewRotation(rotation);
            }
        }
    }
}