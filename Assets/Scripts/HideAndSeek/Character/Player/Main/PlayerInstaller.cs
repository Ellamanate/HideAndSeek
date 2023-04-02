using Zenject;

namespace HideAndSeek
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(Player), typeof(ITickable)).To<Player>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        }
    }
}
