using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HideAndSeek
{
    public class StartGame
    {
        private readonly MainGameMediator _mediator;
        private readonly PlayerInput _playerInput;
        private readonly PlayerUpdateBody _playerBody;
        private readonly GameSceneReferences _sceneReferences;
        private readonly EnemySpawner _enemySpawner;

        public StartGame(MainGameMediator mediator, PlayerInput playerInput, PlayerUpdateBody playerBody,
            GameSceneReferences sceneReferences, EnemySpawner enemySpawner)
        {
            _mediator = mediator;
            _playerInput = playerInput;
            _playerBody = playerBody;
            _sceneReferences = sceneReferences;
            _enemySpawner = enemySpawner;
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

            _mediator.SetAlpha(MainGameMediator.FadeType.Fail, 0);
            _mediator.SetBlockingRaycasts(MainGameMediator.FadeType.Fail, false);

            _playerBody.SetVelocity(Vector3.zero);
            _playerBody.SetPosition(_sceneReferences.PlayerParent.position);
            _playerBody.SetRotation(_sceneReferences.PlayerParent.rotation);
            _enemySpawner.ResetEnemys();

            await _mediator.FadeOut(MainGameMediator.FadeType.Screen, token);

            _playerInput.SetActive(true);

            _ = _mediator.FadeIn(MainGameMediator.FadeType.HUD, token);
        }
    }
}
