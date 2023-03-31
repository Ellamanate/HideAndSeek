using UnityEngine;

namespace HideAndSeek
{
    public class PlayerSpawner
    {
        private readonly Player _player;
        private readonly PlayerModel _model;
        private readonly PlayerConfig _config;
        private readonly GameSceneReferences _sceneReferences;

        public PlayerSpawner(Player player, PlayerModel model, PlayerConfig config, GameSceneReferences sceneReferences)
        {
            _player = player;
            _model = model;
            _config = config;
            _sceneReferences = sceneReferences;
        }

        public void Spawn()
        {
            var body = Object.Instantiate(_config.BodyPrefab, 
                _sceneReferences.PlayerParent.position, 
                _sceneReferences.PlayerParent.rotation, 
                _sceneReferences.PlayerParent);
            
            _player.Initialize(body);
            _player.SetSpeed(_config.Speed);
        }
    }
}
