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

        public void PlayGame(LevelData levelData) => _mainMenu.PlayGame(levelData);
    }
}