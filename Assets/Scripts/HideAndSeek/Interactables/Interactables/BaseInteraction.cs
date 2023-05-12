using UnityEngine;

namespace HideAndSeek
{
    [RequireComponent(typeof(Collider))]
    public class BaseInteraction : MonoBehaviour
    {
        public bool Hitted(RaycastHit hit)
        {
            return hit.collider.gameObject == gameObject;
        }
    }
}