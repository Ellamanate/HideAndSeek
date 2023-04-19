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
        [SerializeField, Tooltip("Скорость в углах в секунду, с которой враг должен вращаться во время поворота лицом к точеке")] 
        private float _speedRotationToDefault = 90;
        [SerializeField, Tooltip("Враг поворачивается к точке не быстрее чем это значение секунд")]
        private float _minTimeRotationToDefault = 0.5f;

        private Transform _enemysParent;

        [Inject]
        private void Construct(GameSceneReferences references)
        {
            _enemysParent = references.EnemysParent;
        }

        public async UniTask PlayAnimation(Enemy enemy, CancellationToken token)
        {
            try
            {
                enemy.SetParent(_rotationPoint);

                Tween tween;

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
                }

                enemy.SetParent(_enemysParent);
            }
            catch
            {
                if (!enemy.Model.Destroyed)
                {
                    enemy.SetParent(_enemysParent);
                }
            }
        }
    }
}