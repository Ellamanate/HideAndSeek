using UnityEngine;

namespace HideAndSeek
{
    public class PlayerBodyFactory
    {
        private readonly PlayerConfig _config;

        public PlayerBodyFactory(PlayerConfig config)
        {
            _config = config;
        }

        public PlayerBody Create(Transform parent, Vector3 position, Quaternion rotation)
        {
            PlayerBody body = Object.Instantiate(_config.BodyPrefab, position, rotation, parent);
            body.Movement.SetSpeed(_config.Speed);
            return body;
        }
    }
}
