namespace HideAndSeek
{
    public class OpenDoor : SwitchDoorState
    {
        private bool _hasClosedDoor;

        protected override void OnInteract(Player player)
        {
            if (_hasClosedDoor)
            {
                OpenDoors();
            }
        }

        protected override void OnConstruct()
        {
            SetActive();
        }

        protected override void OnDoorStateChanged()
        {
            SetActive();
        }

        private void SetActive()
        {
            foreach (var door in Doors)
            {
                if (!door.Opened)
                {
                    _hasClosedDoor = true;
                    LimitInteract.CanPlayerInteract = true;
                    return;
                }
            }

            _hasClosedDoor = false;
            LimitInteract.CanPlayerInteract = false;
        }
    }
}