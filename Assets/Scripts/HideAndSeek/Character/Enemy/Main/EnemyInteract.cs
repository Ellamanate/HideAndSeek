using System.Linq;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyInteract : BaseInteract<Enemy>
    {
        private readonly EnemyModel _model;
        private readonly FailGame _failGame;
        private readonly HidePlayer _hidePlayer;
        private readonly EnemyUpdateBody _updateBody;
        private readonly EnemyUpdateBrain _enemyUpdateBrain;

        public EnemyInteract(EnemyModel model, FailGame failGame, HidePlayer hidePlayer, 
            EnemyUpdateBody updateBody, EnemyUpdateBrain enemyUpdateBrain)
        {
            _model = model;
            _failGame = failGame;
            _hidePlayer = hidePlayer;
            _updateBody = updateBody;
            _enemyUpdateBrain = enemyUpdateBrain;
        }

        protected override InteractorType CurrentInteractorType => InteractorType.Enemy;

        public override bool CheckInteractionAvailable(IInteractable<Enemy> interactable)
        {
            return interactable.LimitInteract.CanEnemyInteract 
                && Physics.Raycast(_updateBody.RaycastPosition, interactable.Position - _updateBody.RaycastPosition, 
                    out var hitInfo, _model.VisionDistance, _model.RaycastLayers)
                && interactable.Hitted(hitInfo);
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
