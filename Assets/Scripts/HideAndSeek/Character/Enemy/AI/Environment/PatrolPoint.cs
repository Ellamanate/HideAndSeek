using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class PatrolPoint : MonoBehaviour
    {
        [SerializeField] private bool _animating = true;
        [SerializeField, ShowIf(nameof(_animating))] private SightAnimation[] _animation;
        [SerializeField, ShowIf(nameof(_animating))] private AnimationData _returnAnimation;
        [SerializeField, HideIf(nameof(_animating)), Tooltip("Время ожидания")] private float _waitTime;

        [SerializeField, Min(0.1f), Tooltip("Время за которое враг развернется по направлению точки, когда встанет на неё")] 
        private float timeToDefaultRotation = 1f;

        private EnemySpawner _spawner;

        [Inject]
        private void Construct(EnemySpawner spawner)
        {
            _spawner = spawner;
        }

        public async UniTask PlayAnimation(string enemyId, CancellationToken token)
        {
            if (_animating && _spawner.TryGetEnemy(enemyId, out Enemy enemy))
            {
                try
                {
                    enemy.SightMovement.SetUpdate(false);

                    await DOTween.To(() => enemy.UpdateBody.Rotation, enemy.UpdateBody.SetRotation,
                        transform.rotation.eulerAngles, timeToDefaultRotation)
                        .AsyncWaitForKill(token);

                    foreach (var animation in _animation)
                    {
                        await enemy.SightMovement.Rotate(
                            animation.LookAt,
                            animation.Ease,
                            animation.SpeedBased
                                ? animation.Speed
                                : animation.Duration,
                            token, animation.SpeedBased);

                        await UniTask.Delay(TimeSpan.FromSeconds(animation.Wait), cancellationToken: token);
                    }

                    await enemy.SightMovement.Rotate(
                        enemy.Model.Rotation,
                        _returnAnimation.Ease,
                        _returnAnimation.SpeedBased
                            ? _returnAnimation.Speed
                            : _returnAnimation.Duration,
                        token, _returnAnimation.SpeedBased);

                    await UniTask.Delay(TimeSpan.FromSeconds(_returnAnimation.Wait), cancellationToken: token);
                }
                finally
                {
                    enemy.SightMovement.SetUpdate(true);
                }
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
            }
        }
    }
}