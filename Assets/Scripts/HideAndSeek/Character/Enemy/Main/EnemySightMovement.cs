using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HideAndSeek.Utils;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class EnemySightMovement : IDisposable, ILateTickable
    {
        private readonly EnemyModel _model;
        private readonly EnemyBody _body;
        private readonly GamePause _pause;

        private Transform _target;
        private Tween _tween;
        private CancellationTokenSource _token;
        private float _speedCorrection;
        private bool _update;
        private bool _animation;

        public EnemySightMovement(EnemyModel model, EnemyBody body, GamePause pause)
        {
            _model = model;
            _body = body;
            _pause = pause;
            _speedCorrection = 1;
        }

        public bool CanUpdate => _update && !_animation;

        public void Reinitialize()
        {
            _speedCorrection = 1;
            _model.SightTargetRotation = Quaternion.identity;
            _model.SightRotation = Quaternion.identity;
            _body.SetViewRotation(Quaternion.identity);
            _token.TryCancel();
            SetUpdate(true);
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void LateTick()
        {
            if (!_pause.Paused)
            {
                _model.SightRotation = _body.SightRotation;

                if (CanUpdate)
                {
                    SetLookAtRotation(_target);
                    Quaternion rotation = Quaternion.Lerp(_model.SightRotation, _model.SightTargetRotation,
                        _body.MaxSightRotationSpeed * _speedCorrection * Time.deltaTime);
                    _body.SetViewRotation(rotation);
                }
            }
        }

        public void SetUpdate(bool update)
        {
            _update = update;
        }

        public void LookAt(Transform target)
        {
            _target = target;

            if (CanUpdate)
            {
                SetLookAtRotation(_target);
            }
        }

        public async UniTask Rotate(Transform lookAt, Ease ease, float duration, CancellationToken token, bool speedBased = false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(GetLookAtRotation(lookAt), Vector3.up);
            await Rotate(targetRotation, ease, duration, token, speedBased);
        }

        public async UniTask Rotate(Quaternion rotation, Ease ease, float duration, CancellationToken token, bool speedBased = false)
        {
            token.CheckCanceled();

            _token.CancelAndDispose();
            _token = CancellationTokenSource.CreateLinkedTokenSource(token);
            _animation = true;
            _model.SightTargetRotation = rotation;

            try
            {
                if (speedBased)
                {
                    await RotateSpeedBased(ease, duration, _token.Token);
                }
                else
                {
                    await Rotate(ease, duration, _token.Token);
                }
            }
            finally
            {
                SetLookAtRotation(_target);

                _animation = false;
            }
        }

        private void CalculateSpeedCorrection()
        {
            float targetAngle = Quaternion.Angle(_model.SightRotation, _model.SightTargetRotation);

            if (targetAngle < _body.MaxSightRotationSpeed)
            {
                _speedCorrection = Mathf.Pow(targetAngle / _body.MaxSightRotationSpeed, 2);
                _speedCorrection = Mathf.Clamp(_speedCorrection, _body.MinSightRotationSpeed, _body.MaxSightRotationSpeed);
            }
        }

        private void SetLookAtRotation(Transform target)
        {
            if (_target != null)
            {
                var direction = GetLookAtRotation(target);
                _model.SightTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                _model.SightTargetRotation = Quaternion.LookRotation(_body.transform.forward, Vector3.up);
            }

            CalculateSpeedCorrection();
        }

        private Vector3 GetLookAtRotation(Transform target)
        {
            return (target.position - _body.transform.position).ChangeY(0);
        }

        private async UniTask Rotate(Ease ease, float duration, CancellationToken token)
        {
            _tween = DOTween.
                To(() => _body.SightRotation, _body.SetViewRotation, _model.SightTargetRotation.eulerAngles, duration)
                .SetEase(ease);

            await _tween.AsyncWaitForKill(token);
        }

        private async UniTask RotateSpeedBased(Ease ease, float speed, CancellationToken token)
        {
            _tween = DOTween
                .To(() => _body.SightRotation, _body.SetViewRotation, _model.SightTargetRotation.eulerAngles, speed)
                .SetEase(ease)
                .SetSpeedBased(true);

            await _tween.AsyncWaitForKill(token);
        }
    }
}
