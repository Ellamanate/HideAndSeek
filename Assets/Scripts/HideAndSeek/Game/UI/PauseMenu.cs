using System.Threading;

namespace HideAndSeek
{
    public class PauseMenu
    {
        private readonly MainGameMediator _mediator;

        public PauseMenu(MainGameMediator mediator)
        { 
            _mediator = mediator;
        }

        public void OpenPauseMenu(CancellationToken token)
        {
            _ = _mediator.FadeIn(MainGameMediator.FadeType.Pause, token);
            _ = _mediator.FadeOut(MainGameMediator.FadeType.HUD, token);
        }

        public void ClosePauseMenu(CancellationToken token)
        {
            _ = _mediator.FadeOut(MainGameMediator.FadeType.Pause, token);
            _ = _mediator.FadeIn(MainGameMediator.FadeType.HUD, token);
        }
    }
}
