using Infrastructure;
using Zenject;

namespace HideAndSeek
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle();

            Container
                .BindFactory<EnemyModel, EnemyBody, Enemy, Enemy.Factory>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemy)
                .AsSingle();
        }

        private void InstallEnemy(DiContainer container, EnemyModel model, EnemyBody body)
        {
            container.Bind<EnemyModel>().FromInstance(model);
            container.Bind<EnemyBody>().FromInstance(body);

            container.Bind(typeof(Enemy), typeof(ITickable)).To<Enemy>().AsSingle();
            container.Bind(typeof(EnemyVision), typeof(ITickable)).To<EnemyVision>().AsSingle().NonLazy();

            container.Bind<SubContainerTickables>().AsSingle();

            var sceneTickables = container.Resolve<SceneTickables>();
            sceneTickables.AddTickable(container.Resolve<SubContainerTickables>());
        }
    }
}
