using System;
using Zenject;

namespace HideAndSeek
{
    public class Enemy : IDisposable, IDestroyable
    {
        public event Action OnDestroyed;

        public readonly EnemyModel Model;
        public readonly EnemyUpdateBody UpdateBody;
        public readonly EnemyMovement Movement;
        public readonly EnemyUpdateBrain Brain;
        public readonly EnemyPatrol Patrol;
        public readonly EnemyVision Vision;
        public readonly EnemySightMovement SightMovement;

        private readonly EnemyBody _body;
        
        public Enemy(EnemyModel model, EnemyBody body, EnemyUpdateBody updateBody, 
            EnemyMovement movement, EnemyUpdateBrain brain, EnemyPatrol patrol,
            EnemyVision vision, EnemySightMovement sightMovement)
        {
            Model = model;
            _body = body;
            UpdateBody = updateBody;
            Movement = movement;
            Brain = brain;
            Patrol = patrol;
            Vision = vision;
            SightMovement = sightMovement;

            _body.OnDestroyed += DestroyEnemy;
        }

        public string Id => Model.Id;

        public void Dispose()
        {
            _body.OnDestroyed -= DestroyEnemy;
        }

        public void Destroy()
        {
            if (!Model.Destroyed)
            {
                _body.Destroy();
                Patrol.CancelLookAround();
            }
        }

        public void Reinitialize()
        {
            Model.Active = true;
            Brain.Reinitialize();
            UpdateBody.Reinitialize();
            Patrol.Reinitialize();
            Vision.Reinitialize();
            SightMovement.Reinitialize();
            Movement.StopMovement();
        }

        public void SetActive(bool active)
        {
            if (!Model.Destroyed && Model.Active != active)
            {
                Model.Active = active;

                if (!active)
                {
                    Movement.StopMovement();
                }
            }
        }

        private void DestroyEnemy()
        {
            Model.Destroyed = true;
            Dispose();
            OnDestroyed?.Invoke();
        }

        public class Factory : PlaceholderFactory<EnemyModel, EnemyBody, EnemySceneConfig, Enemy> { }
    }
}
