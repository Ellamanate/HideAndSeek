using System;
using UnityEngine;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseSensor<T> : MonoBehaviour
    {
        public event Action<T> OnEnter;
        public event Action<T> OnExit;

        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(other, out T component) && EnterValidate(component))
            {
                Enter(component);
                OnEnter?.Invoke(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (TryGetComponent(other, out T component) && ExitValidate(component))
            {
                Exit(component);
                OnExit?.Invoke(component);
            }
        }

        protected abstract bool TryGetComponent(Collider other, out T component);
        protected virtual bool EnterValidate(T component) => true;
        protected virtual bool ExitValidate(T component) => true;
        protected virtual void Enter(T component) { }
        protected virtual void Exit(T component) { }
    }
}
