using HideAndSeek.Utils;
using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class MainGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameSceneConfig _config;
        [SerializeField] private GameSceneReferences _sceneReferences;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainGameInstaller>().FromInstance(this).AsSingle();

            BindData();
            BindMainGame();
        }

        public void Initialize()
        {
            GameInitializer initializer = Container.Resolve<GameInitializer>();
            initializer.Initialize();
        }

        private void BindData()
        {
            Container.Bind<GameSceneReferences>().FromInstance(_sceneReferences).AsSingle();
        }

        private void BindMainGame()
        {
            Container.Bind<GameSceneConfig>().FromInstance(_config).AsSingle();

            Container.BindInterfacesAndSelfTo<SetGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneTickables>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneDisposables>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameMenu>().AsSingle().WithArguments(_sceneReferences.MainMediator);

            Container.Bind<GameInitializer>().AsSingle();
            Container.Bind<SceneInteractions>().AsSingle();
            Container.Bind<MainGame>().AsSingle();
            Container.Bind<StartGame>().AsSingle().WithArguments(_sceneReferences.MainMediator);
            Container.Bind<GameOver>().AsSingle().WithArguments(_sceneReferences.MainMediator);
            Container.Bind<GamePause>().AsSingle();
        }
    }
}