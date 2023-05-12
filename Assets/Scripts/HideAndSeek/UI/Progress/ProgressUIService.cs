using UnityEngine;

namespace HideAndSeek
{
    public class ProgressUIService
    {
        private readonly BasePool<ProgressUI, ProgressUIType> pool;
        private readonly Transform _poolingParent;

        public ProgressUIService(ProgressUIFactory factory, Transform progressPoolingParent)
        {
            pool = new BasePool<ProgressUI, ProgressUIType>(factory);
            _poolingParent = progressPoolingParent;
        }

        public ProgressUI AddProgressTo(ProgressUIType type, Transform target, Vector3 offset)
        {
            var progress = pool.Get(type);
            progress.gameObject.SetActive(true);
            progress.transform.SetParent(target);
            progress.transform.localPosition = offset;

            return progress;
        }

        public void RemoveProgress(ProgressUI progress)
        {
            pool.ReturnToPool(progress);
            progress.gameObject.SetActive(false);
            progress.transform.SetParent(_poolingParent);
        }
    }
}
