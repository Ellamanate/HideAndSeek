using UnityEngine;

namespace HideAndSeek
{
    public class PlayerFactory
    {
        private readonly Player _player;
        private readonly PlayerModel _model;
        private readonly PlayerConfig _config;
        private readonly GameSceneReferences _sceneReferences;

        public PlayerFactory(Player player, PlayerModel model, PlayerConfig config, GameSceneReferences sceneReferences)
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

            _model.Position = _sceneReferences.PlayerParent.position;
            _model.Rotation = _sceneReferences.PlayerParent.rotation;
            _model.Speed = _config.Speed;

            _player.Initialize(body);
        }
    }
}
