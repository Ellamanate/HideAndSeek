using HideAndSeek.AI;
using HideAndSeek.Utils;
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
                .BindFactory<EnemyModel, EnemyBody, EnemySceneConfig, Enemy, Enemy.Factory>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemy)
                .AsSingle();
        }

        private void InstallEnemy(DiContainer container, EnemyModel model, EnemyBody body, EnemySceneConfig sceneConfig)
        {
            container.Bind<EnemyModel>().FromInstance(model);
            container.Bind<EnemyBody>().FromInstance(body);
            container.Bind<EnemySceneConfig>().FromInstance(sceneConfig);

            BindEnemy(container);
            BindBrain(container);
            BindUtils(container);
        }

        private void BindEnemy(DiContainer container)
        {;
            container.Bind<Enemy>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyVision>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemySightMovement>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyMovement>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyInteract>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyUpdateBrain>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyUpdateBody>().AsSingle();
            container.BindInterfacesAndSelfTo<EnemyPatrol>().AsSingle();
        }

        private void BindBrain(DiContainer container)
        {
            container.BindInterfacesTo<OrderActionsFactory>().AsSingle();
            container.BindInterfacesTo<OrderCountersFactory>().AsSingle();
            container.Bind<Actions<OrderActionType>>().AsSingle();
            container.Bind<Execution<OrderActionType, OrderCounterType>>().AsSingle();

            BindOrderActions(container);
            BindOrderScore(container);
        }

        private void BindOrderActions(DiContainer container)
        {
            container.BindFactory<Idle, Idle.Factory>().AsSingle();
            container.BindFactory<Chase, Chase.Factory>().AsSingle();
            container.BindFactory<Search, Search.Factory>().AsSingle();
            container.BindFactory<Patrol, Patrol.Factory>().AsSingle();
        }

        private void BindOrderScore(DiContainer container)
        {
            container.BindFactory<CheckVisible, CheckVisible.Factory>().AsSingle();
            container.BindFactory<CheckSleep, CheckSleep.Factory>().AsSingle();
            container.BindFactory<CheckSearching, CheckSearching.Factory>().AsSingle();
        }

        private static void BindUtils(DiContainer container)
        {
            container.Bind<SubContainerTickables>().AsSingle();
            container.Bind<SubContainerDisposables>().AsSingle();

            var subTickables = container.Resolve<SubContainerTickables>();
            var sceneTickables = container.Resolve<SceneTickables>();
            sceneTickables.AddTickable(subTickables);
            sceneTickables.AddFixedTickable(subTickables);
            sceneTickables.AddLateTickable(subTickables);

            var sceneDisposables = container.Resolve<SceneDisposables>();
            sceneDisposables.AddDisposable(container.Resolve<SubContainerDisposables>());
        }
    }
}
