using Zenject;

namespace HideAndSeek
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenu>().AsSingle();
        }
    }
}