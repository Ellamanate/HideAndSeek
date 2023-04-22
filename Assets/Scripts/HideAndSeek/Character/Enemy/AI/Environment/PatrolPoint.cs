using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [Serializable]
    public struct SightAnimation
    {
        public Transform LookAt;
        public AnimationData Animation;

        public Ease Ease => Animation.Ease;
        public bool SpeedBased => Animation.SpeedBased;
        public float Speed => Animation.Speed;
        public float Duration => Animation.Duration;
        public float Wait => Animation.Wait;
    }

    [Serializable]
    public struct AnimationData
    {
        public Ease Ease;
        public bool SpeedBased;
        [ShowIf(nameof(SpeedBased))] public float Speed;
        [HideIf(nameof(SpeedBased))] public float Duration;
        public float Wait;
    }
    
    public class PatrolPoint : MonoBehaviour
    {
        [SerializeField] private bool _animating = true;
        [SerializeField, ShowIf(nameof(_animating))] private SightAnimation[] _animation;
        [SerializeField, ShowIf(nameof(_animating))] private AnimationData _returnAnimation;
        [SerializeField, HideIf(nameof(_animating)), Tooltip("Время ожидания")] private float _waitTime;

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
                        token, true);

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