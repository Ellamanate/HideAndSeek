using UnityEngine;
using Zenject;

namespace HideAndSeek
{
    public class MainGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private MainGameMediator _mediator;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainGameInstaller>().FromInstance(this).AsSingle();

            BindGame();
        }

        public void Initialize()
        {
            MainGame game = Container.Resolve<MainGame>();
            game.StartGame();
        }

        private void BindGame()
        {
            Container.Bind<MainGame>().AsSingle();
            Container.Bind<StartGame>().AsSingle();
            Container.Bind<MainGameMediator>().FromInstance(_mediator).AsSingle();
        }
    }
}