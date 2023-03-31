using HideAndSeek;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private BootstrapConfig _bootstrapConfig;
        [SerializeField] private ScenesConfig _sceneConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

            BindData();
            BindStates();
            BindLoading();
            BindInput();
        }

        public void Initialize()
        {
            GameStateMachine stateMachine = Container.Resolve<GameStateMachine>();
            stateMachine.MoveToState<BootstrapState>();
        }

        private void BindData()
        {
            Container.Bind<BootstrapConfig>().FromInstance(_bootstrapConfig).AsSingle();
            Container.Bind<ScenesConfig>().FromInstance(_sceneConfig).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<MainGameState>().AsSingle();
            Container.Bind<MenuState>().AsSingle();

            Container.BindFactory<GameStateMachine, BootstrapState, BootstrapState.Factory>();
        }

        private void BindLoading()
        {
            Container.Bind<LoadingScene>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
        }
    }
}