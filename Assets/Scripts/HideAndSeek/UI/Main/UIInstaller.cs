using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private GameSceneReferences _sceneReferences;
        [SerializeField] private ProgressUI _sliderProgressPrefab;
        [SerializeField] private ProgressUI _circleProgressPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerHUD>().AsSingle().WithArguments(_sceneReferences.PlayerHudMediator);
            Container.Bind<PauseMenu>().AsSingle().WithArguments(_sceneReferences.MainMediator);
            Container.Bind<ProgressUIFactory>().AsSingle().WithArguments(_sliderProgressPrefab, _circleProgressPrefab);
            Container.Bind<ProgressUIService>().AsSingle().WithArguments(_sceneReferences.ProgressPoolingParent);
        }
    }
}
