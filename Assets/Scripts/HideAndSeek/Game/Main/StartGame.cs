using HideAndSeek.Extensions;
using System;
using System.Threading;

namespace HideAndSeek
{
    public class StartGame : IDisposable
    {
        private readonly MainGameMediator _mediator;

        private CancellationTokenSource _token;

        public StartGame(MainGameMediator mediator)
        {
            _mediator = mediator;
            _token = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _token.CancelAndDispose();
        }

        public void Start()
        {
            _token = _token.Refresh();
            _mediator.SetFaderAlpha(1);
            _mediator.SetFaderBlockingRaycasts(true);
            _ = _mediator.FadeOut(_token.Token);
        }
    }
}
