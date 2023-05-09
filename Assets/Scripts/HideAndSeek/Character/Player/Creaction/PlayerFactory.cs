using UnityEngine;

namespace HideAndSeek
{
    public class PlayerFactory
    {
        private readonly Player _player;
        private readonly PlayerModel _model;
        private readonly PlayerConfig _config;
        private readonly GameSceneReferences _sceneReferences;
        private readonly MainCamera _mainCamera;

        public PlayerFactory(Player player, PlayerModel model, PlayerConfig config, 
            GameSceneReferences sceneReferences, MainCamera mainCamera)
        {
            _player = player;
            _model = model;
            _config = config;
            _sceneReferences = sceneReferences;
            _mainCamera = mainCamera;
        }

        public void Create()
        {
            var body = Object.Instantiate(_config.BodyPrefab, 
                _sceneReferences.PlayerParent.position, 
                _sceneReferences.PlayerParent.rotation, 
                _sceneReferences.PlayerParent);

            _model.Position = _sceneReferences.PlayerParent.position;
            _model.Rotation = _sceneReferences.PlayerParent.rotation;
            _model.Speed = _config.Speed;
            _model.RaycastDistance = _config.RaycastDistance;
            _model.RaycastLayers = _config.RaycastLayers;
            _model.Visible = true;

            _player.Initialize(body);
            _mainCamera.SetPlayer(body.transform);
        }
    }
}
