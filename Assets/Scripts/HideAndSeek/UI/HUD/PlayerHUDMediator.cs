using UnityEngine;

namespace HideAndSeek
{
    public class PlayerHUDMediator : MonoBehaviour
    {
        private PlayerHUD _hud;

        private void Construct(PlayerHUD hud)
        {
            _hud = hud;
        }
    }
}