using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyFactory
    {
        private readonly Enemy.Factory _enemyFactory;

        private int _spawnIndex;

        public EnemyFactory(Enemy.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public Enemy Create(EnemySceneReferences spawnData, Transform parent)
        {
            var enemyModel = new EnemyModel(_spawnIndex.ToString(), spawnData.Config, spawnData.SpawnPosition.position, spawnData.SpawnPosition.rotation);
            var body = Object.Instantiate(spawnData.Config.EnemyPrefab, parent);

            var sceneConfig = new EnemySceneConfig
            {
                PatrolPositions = spawnData.PatrolPoints.ToArray()
            };

            _spawnIndex++;

            return _enemyFactory.Create(enemyModel, body, sceneConfig);
        }
    }
}
