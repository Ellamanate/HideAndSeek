using UnityEngine;

namespace HideAndSeek
{
    public class GameSceneReferences : MonoBehaviour
    {
        [field: SerializeField] public GameSceneConfig Config { get; private set; }
        [field: SerializeField] public Transform PlayerParent { get; private set; }
    }
}