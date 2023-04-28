using System;
using System.Threading;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class PlayerHUD : IDisposable
    {
        private readonly PlayerHUDMediator _mediator;

        private CancellationTokenSource _token;

        public PlayerHUD(PlayerHUDMediator mediator)
        {
            _mediator = mediator;
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Reinitialize()
        {
            _mediator.SetInteractEnable(0, false);
        }

        public void ShowInteractionIcon()
        {
            _token = _token.Refresh();
            _ = _mediator.ShowInteract(_token.Token);
        }

        public void HideInteractionIcon()
        {
            _token = _token.Refresh();
            _ = _mediator.HideInteract(_token.Token);
        }
    }
}
