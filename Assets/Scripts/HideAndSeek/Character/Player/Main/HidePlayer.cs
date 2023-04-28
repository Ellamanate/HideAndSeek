namespace HideAndSeek
{
    public class HidePlayer
    {
        private readonly PlayerVisibility _playerVisibility;

        public HidePlayer(PlayerVisibility playerVisibility)
        {
            _playerVisibility = playerVisibility;
        }

        public void Hide()
        {
            _playerVisibility.SetInvisible();
        }

        public void Show()
        {
            _playerVisibility.SetVisible();
        }
    }
}
