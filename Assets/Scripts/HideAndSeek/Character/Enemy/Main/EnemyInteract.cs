using System.Linq;

namespace HideAndSeek
{
    public class EnemyInteract : BaseInteract<Enemy>
    {
        private readonly FailGame _failGame;
        private readonly HidePlayer _hidePlayer;
        private readonly EnemyUpdateBrain _enemyUpdateBrain;

        public EnemyInteract(FailGame failGame, HidePlayer hidePlayer, EnemyUpdateBrain enemyUpdateBrain)
        {
            _failGame = failGame;
            _hidePlayer = hidePlayer;
            _enemyUpdateBrain = enemyUpdateBrain;
        }

        protected override InteractorType CurrentInteractorType => InteractorType.Enemy;

        public override bool CheckInteractionAvailable(IInteractable<Enemy> interactable)
        {
            return interactable.LimitInteract.CanEnemyInteract;
        }

        public override void Interact(Enemy enemy)
        {
            if (Interactables.Count > 0)
            {
                if (enemy.Model.PlayerDetectedInShelter)
                {
                    var playersShelter = Interactables.FirstOrDefault(x => Equals(x, _hidePlayer.CurrentShelter));

                    if (playersShelter != null)
                    {
                        playersShelter.Interact(enemy);
                        return;
                    }
                }

                InteractAndLock(enemy, GetValidInteraction());
                _enemyUpdateBrain.UpdateAction();
            }
        }

        public void TouchPlayer(PlayerBody body)
        {
            _failGame.SetFail();
        }
    }
}
