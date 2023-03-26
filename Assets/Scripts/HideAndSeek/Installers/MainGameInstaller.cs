using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class MainGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private MainGameMediator _mediator;
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
            game.StartGame();
        }

        private void BindData()
        {
            Container.Bind<GameSceneReferences>().FromInstance(_sceneReferences).AsSingle();
        }

        private void MainGame()
        {
            Container.Bind<MainGame>().AsSingle();
            Container.BindInterfacesAndSelfTo<UpdateGame>().AsSingle();
        }

        private void BindCharacters()
        {
            Container.Bind<PlayerFactory>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();
            Container.BindFactory<PlayerBody, Player, Player.Factory>().AsSingle();

            Container
                .BindFactory<Player, HideAdsSeekGame, HideAdsSeekGame.Factory>()
                .FromSubContainerResolve()
                .ByMethod(InstallHideAndSeek)
                .AsSingle();
        }

        private void InstallHideAndSeek(DiContainer container, Player player)
        {
            container.Bind<Player>().FromInstance(player).AsSingle();
            container.Bind<HideAdsSeekGame>().AsSingle();
            container.Bind<StartGame>().AsSingle();
            container.Bind<PlayerInput>().AsSingle();
            container.Bind<MainGameMediator>().FromInstance(_mediator).AsSingle();
        }
    }
}