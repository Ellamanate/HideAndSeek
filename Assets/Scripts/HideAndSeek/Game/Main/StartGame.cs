using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HideAndSeek
{
    public class StartGame
    {
        private readonly MainGameMediator _mediator;
        private readonly PlayerInput _playerInput;
        private readonly Player _player;
        private readonly GameSceneReferences _sceneReferences;

        public StartGame(MainGameMediator mediator, PlayerInput playerInput, Player player, GameSceneReferences sceneReferences)
        {
            _mediator = mediator;
            _playerInput = playerInput;
            _player = player;
            _sceneReferences = sceneReferences;
        }
        
        public async UniTask Start(CancellationToken token)
        {
            _mediator.SetAlpha(MainGameMediator.FadeType.Screen, 1);
            _mediator.SetBlockingRaycasts(MainGameMediator.FadeType.Screen, true);

            _mediator.SetAlpha(MainGameMediator.FadeType.Complete, 0);
            _mediator.SetBlockingRaycasts(MainGameMediator.FadeType.Complete, false);

            _mediator.SetAlpha(MainGameMediator.FadeType.Pause, 0);
            _mediator.SetBlockingRaycasts(MainGameMediator.FadeType.Pause, false);

            _mediator.SetAlpha(MainGameMediator.FadeType.HUD, 0);
            _mediator.SetBlockingRaycasts(MainGameMediator.FadeType.HUD, false);

            _player.SetMovementDirection(Vector3.zero);
            _player.SetPosition(_sceneReferences.PlayerParent.position);
            _player.SetRotation(_sceneReferences.PlayerParent.rotation);

            await _mediator.FadeOut(MainGameMediator.FadeType.Screen, token);

            _playerInput.SetActive(true);

            await _mediator.FadeIn(MainGameMediator.FadeType.HUD, token);
        }
    }
}
