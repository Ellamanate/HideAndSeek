using System;

namespace HideAndSeek
{
    public class PlayerSpawner
    {
        private readonly PlayerFactory _factory;
        private readonly UpdateGame _updateGame;
        private readonly GameSceneReferences _sceneReferences;

        public PlayerSpawner(PlayerFactory factory, UpdateGame updateGame, GameSceneReferences sceneReferences)
        {
            _factory = factory;
            _updateGame = updateGame;
            _sceneReferences = sceneReferences;
        }

        public Player Spawn()
        {
            Player player = _factory.Create(_sceneReferences.PlayerParent, _sceneReferences.PlayerParent.position, _sceneReferences.PlayerParent.rotation);
            player.OnDestroyed += DestroyAction;
            _updateGame.AddFixedTickable(player);

            return player;

            void DestroyAction() => Destroy(player, DestroyAction);
        }

        private void Destroy(Player player, Action action)
        {
            _updateGame.RemoveFixedTickable(player);
            player.OnDestroyed -= action;
        }
    }
}
