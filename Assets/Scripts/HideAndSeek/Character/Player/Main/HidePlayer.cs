using HideAndSeek.Utils;

namespace HideAndSeek
{
    public class HidePlayer
    {
        private readonly PlayerVisibility _playerVisibility;
        private readonly MainCamera _mainCamera;

        public Shelter CurrentShelter { get; private set; }

        public HidePlayer(PlayerVisibility playerVisibility, MainCamera mainCamera)
        {
            _playerVisibility = playerVisibility;
            _mainCamera = mainCamera;
        }

        public bool HasShelter => CurrentShelter != null;

        public bool CanCatchInCurrentShelter(Enemy enemy)
        {
            return HasShelter
                && (enemy.Model.PlayerDetectedInShelter
                || Utilities.GetChance(enemy.Model.CatchShelterChance));
        }

        public void Hide(Shelter shelter)
        {
            if (CurrentShelter != null)
            {
                GameLogger.LogError("Player already hided");
                return;
            }

            CurrentShelter = shelter;
            _mainCamera.LookAtShelter(CurrentShelter);
            _playerVisibility.SetInvisible();
        }

        public void Show()
        {
            CurrentShelter = null;
            _mainCamera.LookAtPlayer();
            _playerVisibility.SetVisible();
        }
    }
}
