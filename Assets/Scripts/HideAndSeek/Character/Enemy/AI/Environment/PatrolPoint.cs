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
        [SerializeField, HideIf(nameof(_animating))] private bool _rotateBody;
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
            _spawner.TryGetEnemy(enemyId, out Enemy enemy);

            if (_animating)
            {
                await RotationAnimation(enemy, token);
            }
            else
            {
                if (_rotateBody)
                {
                    await DOTween.To(() => enemy.UpdateBody.Rotation, enemy.UpdateBody.SetRotation,
                        transform.rotation.eulerAngles, timeToDefaultRotation)
                        .AsyncWaitForKill(token);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
            }
        }

        private async UniTask RotationAnimation(Enemy enemy, CancellationToken token)
        {
            try
            {
                enemy.SightMovement.SetUpdate(false);

                await DOTween.To(() => enemy.UpdateBody.Rotation, enemy.UpdateBody.SetRotation,
                    transform.rotation.eulerAngles, timeToDefaultRotation)
                    .AsyncWaitForKill(token);
               
                foreach (var animation in _animation)
                {
                    await Rotate(enemy, animation.LookAt, animation.Animation, token);
                    await UniTask.Delay(TimeSpan.FromSeconds(animation.Wait), cancellationToken: token);
                }

                await Rotate(enemy, enemy.Model.Rotation, _returnAnimation, token);
                await UniTask.Delay(TimeSpan.FromSeconds(_returnAnimation.Wait), cancellationToken: token);
            }
            finally
            {
                enemy.SightMovement.SetUpdate(true);
            }
        }

        private async UniTask Rotate(Enemy enemy, Transform lookAt, AnimationData animation, CancellationToken token)
        {
            await enemy.SightMovement.Rotate(lookAt,
                animation.Ease,
                animation.SpeedBased
                    ? animation.Speed
                    : animation.Duration,
                token, animation.SpeedBased);
        }

        private async UniTask Rotate(Enemy enemy, Quaternion rotation, AnimationData animation, CancellationToken token)
        {
            await enemy.SightMovement.Rotate(rotation,
                animation.Ease,
                animation.SpeedBased
                    ? animation.Speed
                    : animation.Duration,
                token, animation.SpeedBased);
        }
    }
}