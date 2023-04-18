using Zenject;

namespace HideAndSeek
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayer();
            BindCamera();
        }

        private void BindPlayer()
        {
            Container.Bind(typeof(Player), typeof(ITickable)).To<Player>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }

        private void BindCamera()
        {
            Container.BindInterfacesAndSelfTo<MainCamera>().AsSingle();
            Container.Bind<MoveCamera>().AsSingle();
        }
    }
}
