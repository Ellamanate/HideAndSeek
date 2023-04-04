using System.Linq;
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

        public Enemy Create(EnemySceneReferences spawnData, Transform parent)
        {
            var enemyModel = new EnemyModel(spawnData.Config, spawnData.SpawnPosition.position, spawnData.SpawnPosition.rotation);
            var body = Object.Instantiate(spawnData.Config.EnemyPrefab, parent);

            var sceneConfig = new EnemySceneConfig
            {
                PatrolPositions = spawnData.PatrolPoints.Where(x => x != null).Select(x => x.position).ToArray()
            };

            var enemy = _enemyFactory.Create(enemyModel, body, sceneConfig);

            return enemy;
        }
    }
}
