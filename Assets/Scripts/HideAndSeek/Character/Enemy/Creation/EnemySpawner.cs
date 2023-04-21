using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HideAndSeek
{
    public class EnemySpawner
    {
        private class EnemySpawnData
        {
            public readonly Enemy Enemy;
            public readonly EnemyModel DefaultModel;

            public EnemySpawnData(Enemy enemy, EnemyModel defaultModel)
            {
                Enemy = enemy;
                DefaultModel = defaultModel;
            }
        } 

        private readonly EnemyFactory _factory;
        private readonly GameSceneReferences _references;
        private readonly List<Enemy> _enemys;
        private readonly ReadOnlyCollection<Enemy> _readonlyEnemys;
        private readonly Dictionary<string, EnemySpawnData> _spawnedEnemys;

        public EnemySpawner(EnemyFactory factory, GameSceneReferences references)
        {
            _factory = factory;
            _references = references;
            _enemys = new List<Enemy>();
            _readonlyEnemys = new ReadOnlyCollection<Enemy>(_enemys);
            _spawnedEnemys = new Dictionary<string, EnemySpawnData>();
        }

        public IReadOnlyCollection<Enemy> Enemys => _readonlyEnemys;

        public void Spawn()
        {
            foreach (var enemyData in _references.Enemys)
            {
                Enemy enemy = _factory.Create(enemyData, _references.EnemysParent);

                AddEnemy(enemy);

                enemy.OnDestroyed += DestroyAction;

                void DestroyAction() => Destroy(enemy, DestroyAction);
            }
        }

        public void ResetEnemys()
        {
            foreach (var spawnData in _spawnedEnemys.Values)
            {
                spawnData.Enemy.Model.CopyFrom(spawnData.DefaultModel);
                spawnData.Enemy.Reinitialize();
                spawnData.Enemy.Brain.UpdateAction();
            }
        }

        public bool TryGetEnemy(string id, out Enemy enemy)
        {
            if (_spawnedEnemys.TryGetValue(id, out EnemySpawnData spawnData))
            {
                enemy = spawnData.Enemy;
                return true;
            }

            enemy = null;
            return false;
        }

        private void Destroy(Enemy enemy, Action action)
        {
            RemoveEnemy(enemy);

            enemy.OnDestroyed -= action;
        }

        private void AddEnemy(Enemy enemy)
        {
            if (!_spawnedEnemys.ContainsKey(enemy.Id))
            {
                _enemys.Add(enemy);
                _spawnedEnemys.Add(enemy.Id, new EnemySpawnData(enemy, new EnemyModel(enemy.Model)));
            }
        }

        private void RemoveEnemy(Enemy enemy)
        {
            _enemys.Remove(enemy);
            _spawnedEnemys.Remove(enemy.Id);
        }
    }
}