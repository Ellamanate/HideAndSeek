using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HideAndSeek.AI;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class EnemyUpdateBrain : IDisposable
    {
        private readonly EnemyModel _model;
        private readonly EnemyBody _body;
        private readonly Actions<OrderActionType> _actions;
        private readonly Execution<OrderActionType, OrderCounterType> _execution;
        private CancellationTokenSource _token;

        public EnemyUpdateBrain(EnemyModel model, EnemyBody body, Actions<OrderActionType> actions,
            Execution<OrderActionType, OrderCounterType> execution)
        {
            _model = model;
            _body = body;
            _actions = actions;
            _execution = execution;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Reinitialize()
        {
            _actions.Initialize(_model.ActionTypes);
            _execution.Initialize(_model.CounterTypes);
            _token.TryCancel();
        }

        public void UpdateAction()
        {
            if (!_model.Destroyed && _model.Active)
            {
                _execution.Execute();
            }
        }

        public void SetAttentiveness(AttentivenessType attentiveness)
        {
            if (!_model.Destroyed && _model.Active && attentiveness != _model.CurrentAttentiveness)
            {
                UpdateAttentiveness(attentiveness);
            }
        }

        public void CacelRelaxTransition()
        {
            _token.TryCancel();
        }

        private void UpdateAttentiveness(AttentivenessType attentiveness)
        {
            _model.CurrentAttentiveness = attentiveness;
            _body.Movement.SetMaxSpeed(_model.Speed);
            _body.VisionCone.SetConeData(_model.ConeData);

            if (_model.CurrentAttentiveness == AttentivenessType.Seaching)
            {
                _token = _token.Refresh();
                _ = Timer(AttentivenessType.Relax, _token.Token);
            }

            GameLogger.Log($"Attentiveness: {attentiveness}");
        }
        
        private async UniTask Timer(AttentivenessType targetType, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_model.AttentivenesDeclineTime), cancellationToken: token);

            SetAttentiveness(targetType);
        }
    }
}
