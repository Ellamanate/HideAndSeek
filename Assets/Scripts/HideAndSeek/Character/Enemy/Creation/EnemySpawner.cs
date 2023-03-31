namespace HideAndSeek
{
    public class EnemySpawner
    {
        private readonly EnemyFactory _factory;
        private readonly GameSceneReferences _references;

        public EnemySpawner(EnemyFactory factory, GameSceneReferences references)
        {
            _factory = factory;
            _references = references;
        }

        public void Spawn()
        {
            foreach (var enemyData in _references.Enemys)
            {
                _factory.Create(enemyData.Config, _references.EnemysParent, enemyData.Position.position, enemyData.Position.rotation);
            }
        }
    }
}