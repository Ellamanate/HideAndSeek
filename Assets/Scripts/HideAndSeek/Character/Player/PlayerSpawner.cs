using System;

namespace HideAndSeek
{
    public class PlayerSpawner
    {
        private readonly PlayerFactory _factory;
        private readonly GameSceneReferences _sceneReferences;

        public PlayerSpawner(PlayerFactory factory, GameSceneReferences sceneReferences)
        {
            _factory = factory;
            _sceneReferences = sceneReferences;
        }

        public Player Spawn()
        {
            Player player = _factory.Create(_sceneReferences.PlayerParent, _sceneReferences.PlayerParent.position, _sceneReferences.PlayerParent.rotation);
            player.OnDestroyed += DestroyAction;

            return player;

            void DestroyAction() => Destroy(player, DestroyAction);
        }

        private void Destroy(Player player, Action action)
        {
            player.OnDestroyed -= action;
        }
    }
}
