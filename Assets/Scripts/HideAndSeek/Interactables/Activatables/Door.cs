using Cysharp.Threading.Tasks;
using HideAndSeek.Utils;
using Sirenix.OdinInspector;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace HideAndSeek
{
    public class Door : MonoBehaviour, IResettable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshObstacle _obstacle;
        [SerializeField] private bool _openAtStart;
        [SerializeField] private string _openingKey;
        [SerializeField] private string _openingState = "OpenDoor";
        [SerializeField] private string _closingState = "CloseDoor";

        private CancellationTokenSource _token;

        public bool Opened { get; private set; }

        private void Awake()
        {
            ToDefault();
        }

        private void OnDestroy()
        {
            _token.CancelAndDispose();
        }

        public async UniTask Open(CancellationToken token = default)
        {
            SetOpenState(true);

            _animator.SetBool(_openingKey, true);
            _token = _token.Refresh();
            _token.AddTo(token);

            await UniTask.WaitUntilCanceled(token);
        }

        public async UniTask Close(CancellationToken token = default)
        {
            SetOpenState(false);

            _animator.SetBool(_openingKey, false);
            _token = _token.Refresh();
            _token.AddTo(token);

            await UniTask.WaitUntilCanceled(token);
        }

        public void ToDefault()
        {
            Opened = _openAtStart;
            _animator.Play(Opened ? _openingState : _closingState, 0, 1);
            _animator.SetBool(_openingKey, Opened);
            _obstacle.enabled = !Opened;
        }

        public void OnAnimationEnded()
        {
            _token.TryCancel();
        }

        private void SetOpenState(bool open)
        {
            Opened = open;
            _obstacle.enabled = !open;
        }

#if UNITY_EDITOR
        [Button("Open")]
        public void DebugOpen()
        {
            _ = Open();
        }

        [Button("Close")]
        public void DebugClose()
        {
            _ = Close();
        }
#endif
    }
}