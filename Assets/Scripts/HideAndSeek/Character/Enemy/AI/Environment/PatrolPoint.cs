using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    [RequireComponent(typeof(Animation))]
    public class PatrolPoint : MonoBehaviour
    {
        [SerializeField] private bool _animating = true;
        [SerializeField, ShowIf(nameof(_animating))] private Animation _animation;
        [SerializeField, HideIf(nameof(_animating))] private float _waitTime;
        [SerializeField] private Transform _rotationPoint;
        [SerializeField, Tooltip("Скорость в углах в секунду, с которой враг должен вращаться чтобы встать лицом к точеке")] 
        private float _speedRotationToDefault = 90;
        [SerializeField, Tooltip("Враг поворачивается к точке не быстрее чем это значение секунд")]
        private float _minTimeRotationToDefault = 0.5f;

        private EnemySpawner _spawner;

        [Inject]
        private void Construct(EnemySpawner spawner)
        {
            _spawner = spawner;
        }

        public async UniTask PlayAnimation(string enemyId, CancellationToken token)
        {
            if (_spawner.TryGetEnemy(enemyId, out Enemy enemy))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: token);
            }

            /*Tween tween;

            if (Quaternion.Angle(enemy.Rotation, transform.rotation) > _speedRotationToDefault)
            {
                tween = DOTween
                    .To(() => enemy.Rotation, enemy.SetRotation, transform.eulerAngles, _speedRotationToDefault)
                    .SetSpeedBased(true);
            }
            else
            {
                tween = DOTween.To(() => enemy.Rotation, enemy.SetRotation, transform.eulerAngles,
                    _minTimeRotationToDefault);
            }

            tween.SetEase(Ease.Linear);

            await tween.AsyncWaitForKill(token);
            
            if (_animating && _animation.clip != null)
            {
                _animation.Play();

                await UniTask.Delay(TimeSpan.FromSeconds(_animation.clip.length), cancellationToken: token);
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
            }*/
        }
    }
}