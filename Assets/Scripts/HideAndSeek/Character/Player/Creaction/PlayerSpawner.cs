namespace HideAndSeek
{
    public class PlayerSpawner
    {
        private readonly Player _player;
        private readonly PlayerBodyFactory _factory;
        private readonly GameSceneReferences _sceneReferences;

        public PlayerSpawner(Player player, PlayerBodyFactory factory, GameSceneReferences sceneReferences)
        {
            _player = player;
            _factory = factory;
            _sceneReferences = sceneReferences;
        }

        public PlayerBody Spawn()
        {
            PlayerBody player = _factory.Create(_sceneReferences.PlayerParent, _sceneReferences.PlayerParent.position, _sceneReferences.PlayerParent.rotation);
            _player.Initialize(player);
            return player;
        }
    }
}
