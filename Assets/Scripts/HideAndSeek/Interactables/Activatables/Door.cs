using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace HideAndSeek
{
    public class Door : MonoBehaviour, IResettable
    {
        [SerializeField] private Transform _door;

        public bool Opened { get; private set; }

        public async UniTask Open(CancellationToken token = default)
        {
            Opened = true;
            _door.gameObject.SetActive(!Opened);
        }

        public async UniTask Close(CancellationToken token = default)
        {
            Opened = false;
            _door.gameObject.SetActive(!Opened);
        }

        public void ToDefault()
        {
            Opened = false;
        }
    }
}