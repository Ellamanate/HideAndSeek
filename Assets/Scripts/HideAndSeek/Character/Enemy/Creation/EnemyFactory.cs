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
            var enemyModel = new EnemyModel(config, position, rotation);
            var body = Object.Instantiate(config.EnemyPrefab, parent);
            var enemy = _enemyFactory.Create(enemyModel, body);

            enemy.Initialize();

            return enemy;
        }
    }
}
