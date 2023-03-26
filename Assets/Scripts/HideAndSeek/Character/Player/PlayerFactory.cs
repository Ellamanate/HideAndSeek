using UnityEngine;

namespace HideAndSeek
{
    public class PlayerFactory
    {
        private readonly Player.Factory _factory;
        private readonly PlayerConfig _config;

        public PlayerFactory(Player.Factory factory, PlayerConfig config)
        {
            _factory = factory;
            _config = config;
        }

        public Player Create(Transform parent, Vector3 position, Quaternion rotation)
        {
            PlayerBody body = Object.Instantiate(_config.BodyPrefab, position, rotation, parent);
            return _factory.Create(body);
        }
    }
}
