namespace HideAndSeek
{
    public class CloseDoor : SwitchDoorState
    {
        private bool _hasOpenedDoor;

        protected override void OnInteract(Player player)
        {
            if (_hasOpenedDoor)
            {
                CloseDoors();
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
                if (door.Opened)
                {
                    _hasOpenedDoor = true;
                    LimitInteract.CanPlayerInteract = true;
                    return;
                }
            }

            _hasOpenedDoor = false;
            LimitInteract.CanPlayerInteract = false;
        }
    }
}