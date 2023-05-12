using System;
using System.Threading;
using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class PlayerHUD : IDisposable
    {
        private readonly PlayerHUDMediator _mediator;

        private CancellationTokenSource _token;

        public bool InteractablerShown { get; private set; }

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
            InteractablerShown = false;
            _mediator.SetInteractEnable(0, false);
            _mediator.SetActiveInteractionHint(false);
        }

        public void SetInteraction(IPositionedInteraction interaction)
        {
            if (interaction != null)
            {
                _mediator.SetActiveInteractionHint(true);
                _mediator.SetInteractionHintTarget(interaction);
            }
            else
            {
                _mediator.SetActiveInteractionHint(false);
            }
        }

        public void ShowInteraction(IPositionedInteraction interaction)
        {
            if (interaction != null)
            {
                SetInteraction(interaction);
                ShowInteractionIcon();
            }
        }

        public void HideInteraction()
        {
            _mediator.SetActiveInteractionHint(false);
            HideInteractionIcon();
        }

        private void ShowInteractionIcon()
        {
            if (!InteractablerShown)
            {
                InteractablerShown = true;
                _token = _token.Refresh();
                _ = _mediator.ShowInteract(_token.Token);
            }
        }

        private void HideInteractionIcon()
        {
            if (InteractablerShown)
            {
                InteractablerShown = false;
                _token = _token.Refresh();
                _ = _mediator.HideInteract(_token.Token);
            }
        }
    }
}
