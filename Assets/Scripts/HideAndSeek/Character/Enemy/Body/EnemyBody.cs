using System;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyBody : MonoBehaviour, IDestroyable
    {
        public event Action OnDestroyed;
        
        [field: SerializeField] public EnemyMovement Movement { get; private set; }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}