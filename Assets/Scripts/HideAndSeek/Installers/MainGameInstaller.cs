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
            MainGame();
            BindCharacters();
        }

        public void Initialize()
        {
            MainGame game = Container.Resolve<MainGame>();
            game.Initialize();
        }

        private void BindData()
        {
            Container.Bind<GameSceneReferences>().FromInstance(_sceneReferences).AsSingle();
        }

        private void MainGame()
        {
            Container.BindInterfacesAndSelfTo<MainGame>().AsSingle();
            Container.Bind<StartGame>().AsSingle().WithArguments(_sceneReferences.Mediator);
            Container.Bind<GameOver>().AsSingle().WithArguments(_sceneReferences.Mediator);
            Container.Bind<PauseMenu>().AsSingle().WithArguments(_sceneReferences.Mediator);
            Container.Bind<GamePause>().AsSingle();

            Container.Bind<DetectEndGame>().AsSingle().NonLazy();
        }

        private void BindCharacters()
        {
            Container.Bind<Player>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }
    }
}