using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class MenuMediator : MonoBehaviour
    {
        private MainMenu _mainMenu;

        [Inject]
        private void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        [Button] public void PlayGame() => _mainMenu.PlayGame();
    }
}