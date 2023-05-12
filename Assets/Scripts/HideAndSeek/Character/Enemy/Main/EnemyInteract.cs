using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using System.Linq;
using System.Threading;
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

        private CancellationTokenSource _token;

        public EnemyInteract(EnemyModel model, FailGame failGame, HidePlayer hidePlayer, 
            EnemyUpdateBody updateBody, EnemyUpdateBrain enemyUpdateBrain)
        {
            _model = model;
            _failGame = failGame;
            _hidePlayer = hidePlayer;
            _updateBody = updateBody;
            _enemyUpdateBrain = enemyUpdateBrain;
        }

        protected override void OnDisposed()
        {
            _token.CancelAndDispose();
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

                var interaction = GetValidInteraction();

                if (interaction is IAnimatableInteraction<Enemy> animatable)
                {
                    _token = _token.Refresh();
                    _ = InteractAnimation(animatable, interaction, enemy, _token.Token);
                }
                else
                {
                    _token.TryCancel();
                    InteractAndLock(enemy, interaction);
                    _enemyUpdateBrain.UpdateAction();
                }
            }
        }

        public void TouchPlayer(PlayerBody body)
        {
            _failGame.SetFail();
        }

        private async UniTask InteractAnimation(IAnimatableInteraction<Enemy> animatable,
            IInteractable<Enemy> interaction, Enemy enemy, CancellationToken token)
        {
            await animatable.PlayInteractionAnimation(enemy, token);

            InteractAndLock(enemy, interaction);
            _enemyUpdateBrain.UpdateAction();
        }
    }
}
