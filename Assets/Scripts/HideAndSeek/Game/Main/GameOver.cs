﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HideAndSeek
{
    public class GameOver
    {
        private readonly MainGameMediator _mediator;
        private readonly Player _player;
        private readonly PlayerInput _playerInput;
        private readonly EnemySpawner _enemySpawner;

        public GameOver(MainGameMediator mediator, Player player, PlayerInput playerInput, EnemySpawner enemySpawner)
        {
            _mediator = mediator;
            _player = player;
            _playerInput = playerInput;
            _enemySpawner = enemySpawner;
        }

        public async UniTask FailGame(CancellationToken token)
        {
            _playerInput.SetActive(false);
            _player.SetVelocity(Vector3.zero);

            DisableEnemys();

            _ = _mediator.FadeOut(MainGameMediator.FadeType.HUD, token);

            await _mediator.FadeIn(MainGameMediator.FadeType.Screen, token);
            await _mediator.FadeIn(MainGameMediator.FadeType.Fail, token);
        }

        public async UniTask CompleteGame(CancellationToken token)
        {
            _playerInput.SetActive(false);
            _player.SetVelocity(Vector3.zero);

            DisableEnemys();

            _ = _mediator.FadeOut(MainGameMediator.FadeType.HUD, token);

            await _mediator.FadeIn(MainGameMediator.FadeType.Screen, token);
            await _mediator.FadeIn(MainGameMediator.FadeType.Complete, token);
        }

        private void DisableEnemys()
        {
            foreach (var enemy in _enemySpawner.Enemys)
            {
                enemy.SetActive(false);
            }
        }
    }
}
