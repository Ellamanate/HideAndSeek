using Zenject;

namespace HideAndSeek
{
    public class MainGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainGame>().AsSingle();
            Container.Bind<StartGame>().AsSingle();
        }
    }
}