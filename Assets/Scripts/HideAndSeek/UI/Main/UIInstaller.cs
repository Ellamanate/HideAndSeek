using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private GameSceneReferences _sceneReferences;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerHUD>().AsSingle().WithArguments(_sceneReferences.PlayerHudMediator);
            Container.Bind<PauseMenu>().AsSingle().WithArguments(_sceneReferences.MainMediator);
        }
    }
}
