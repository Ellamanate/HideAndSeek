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
            BindGame();
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

        private void BindCharacters()
        {
            Container.Bind<PlayerFactory>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle();
            Container.BindFactory<PlayerBody, Player, Player.Factory>().AsSingle();
        }

        private void BindGame()
        {
            Container.Bind<MainGame>().AsSingle();
            Container.Bind<StartGame>().AsSingle();
            Container.Bind<UpdateGame>().AsSingle();
            Container.Bind<MainGameMediator>().FromInstance(_mediator).AsSingle();
        }
    }
}