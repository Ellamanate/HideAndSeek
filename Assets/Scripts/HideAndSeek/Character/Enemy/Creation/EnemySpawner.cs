using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HideAndSeek
{
    public class EnemySpawner
    {
        private readonly EnemyFactory _factory;
        private readonly GameSceneReferences _references;
        private readonly List<Enemy> _enemys;
        private readonly ReadOnlyCollection<Enemy> _readonlyEnemys;
        private readonly Dictionary<Enemy, EnemyModel> _spawnedEnemys;

        public EnemySpawner(EnemyFactory factory, GameSceneReferences references)
        {
            _factory = factory;
            _references = references;
            _enemys = new List<Enemy>();
            _readonlyEnemys = new ReadOnlyCollection<Enemy>(_enemys);
            _spawnedEnemys = new Dictionary<Enemy, EnemyModel>();
        }

        public IReadOnlyCollection<Enemy> Enemys => _readonlyEnemys;

        public void Spawn()
        {
            foreach (var enemyData in _references.Enemys)
            {
                Enemy enemy = _factory.Create(enemyData.Config, _references.EnemysParent, enemyData.Position.position, enemyData.Position.rotation);

                AddEnemy(enemy);

                enemy.OnDestroyed += DestroyAction;

                void DestroyAction() => Destroy(enemy, DestroyAction);
            }
        }

        public void ResetEnemys()
        {
            foreach (var keyValue in _spawnedEnemys)
            {
                keyValue.Key.Model.CopyFrom(keyValue.Value);
                keyValue.Key.Initialize();
            }
        }

        private void Destroy(Enemy enemy, Action action)
        {
            RemoveEnemy(enemy);

            enemy.OnDestroyed -= action;
        }

        private void AddEnemy(Enemy enemy)
        {
            if (!_enemys.Contains(enemy))
            {
                _enemys.Add(enemy);
                _spawnedEnemys.Add(enemy, new EnemyModel(enemy.Model));
            }
        }

        private void RemoveEnemy(Enemy enemy)
        {
            _enemys.Remove(enemy);
            _spawnedEnemys.Remove(enemy);
        }
    }
}