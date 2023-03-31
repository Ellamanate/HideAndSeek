using UnityEngine;

namespace HideAndSeek
{
    public class EnemyFactory
    {
        private readonly Enemy.Factory _enemyFactory;

        public EnemyFactory(Enemy.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public Enemy Create(EnemyConfig config, Transform parent, Vector3 position, Quaternion rotation)
        {
            var enemyModel = new EnemyModel();
            var body = Object.Instantiate(config.EnemyPrefab, position, rotation, parent);
            var enemy = _enemyFactory.Create(enemyModel, body);

            enemy.SetSpeed(config.Speed);
            enemyModel.RaycastLayers = config.RaycastLayers;
            enemyModel.VisionDistance = config.VisionDistance;

            return enemy;
        }
    }
}
