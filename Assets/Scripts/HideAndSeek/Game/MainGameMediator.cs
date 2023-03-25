using Zenject;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HideAndSeek
{
    public class MainGameMediator : MonoBehaviour
    {
        private MainGame _mainGame;

        [Inject]
        private void Construct(MainGame mainGame)
        {
            _mainGame = mainGame;
        }

        [Button] public void Exit() => _mainGame.Exit();
    }
}