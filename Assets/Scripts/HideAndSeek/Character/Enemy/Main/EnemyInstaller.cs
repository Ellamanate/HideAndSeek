using System;
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
                .BindFactory<EnemyModel, EnemyBody, Enemy, Enemy.Factory>()
                .FromSubContainerResolve()
                .ByMethod(InstallEnemy)
                .AsSingle();
        }

        private void InstallEnemy(DiContainer container, EnemyModel model, EnemyBody body)
        {
            container.Bind<EnemyModel>().FromInstance(model);
            container.Bind<EnemyBody>().FromInstance(body);

            BindEnemy(container);
            BindBrain(container);
            BindUtils(container);
        }

        private void BindEnemy(DiContainer container)
        {;
            container.Bind(typeof(Enemy), typeof(ITickable)).To<Enemy>().AsSingle();
            container.Bind(typeof(EnemyVision), typeof(ITickable)).To<EnemyVision>().AsSingle().NonLazy();
            container.Bind(typeof(EnemyMovement), typeof(IDisposable)).To<EnemyMovement>().AsSingle().NonLazy();
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
            container
                .BindFactory<CheckVisible, CheckVisible.Factory>()
                .AsSingle();
        }

        private static void BindUtils(DiContainer container)
        {
            container.Bind<SubContainerTickables>().AsSingle();
            container.Bind<SubContainerDisposables>().AsSingle();

            var sceneTickables = container.Resolve<SceneTickables>();
            sceneTickables.AddTickable(container.Resolve<SubContainerTickables>());

            var sceneDisposables = container.Resolve<SceneDisposables>();
            sceneDisposables.AddDisposable(container.Resolve<SubContainerDisposables>());
        }
    }
}
