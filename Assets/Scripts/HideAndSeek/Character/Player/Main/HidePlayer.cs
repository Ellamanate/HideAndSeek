namespace HideAndSeek
{
    public class HidePlayer
    {
        private readonly PlayerVisibility _playerVisibility;

        public Shelter CurrentShelter { get; private set; }

        public HidePlayer(PlayerVisibility playerVisibility)
        {
            _playerVisibility = playerVisibility;
        }

        public bool HasShelter => CurrentShelter != null;

        public void Hide(Shelter shelter)
        {
            if (CurrentShelter != null)
            {
                GameLogger.LogError("Player already hided");
                return;
            }

            CurrentShelter = shelter;
            _playerVisibility.SetInvisible();
        }

        public void Show()
        {
            CurrentShelter = null;
            _playerVisibility.SetVisible();
        }
    }
}
