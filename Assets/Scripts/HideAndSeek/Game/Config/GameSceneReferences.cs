using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HideAndSeek
{
    public class GameSceneReferences : MonoBehaviour
    {
        [field: SerializeField, FoldoutGroup("General")] public MainGameMediator MainMediator { get; private set; }
        [field: SerializeField, FoldoutGroup("General")] public PlayerHUDMediator PlayerHudMediator { get; private set; }
        [field: SerializeField, FoldoutGroup("General")] public CinemachineVirtualCamera VirtualCamera { get; private set; }
        [field: SerializeField, FoldoutGroup("Player")] public Transform PlayerParent { get; private set; }
        [field: SerializeField, FoldoutGroup("Enemys")] public Transform EnemysParent { get; private set; }
        [field: SerializeField, FoldoutGroup("Enemys")] public EnemySceneReferences[] Enemys { get; private set; }
    }
}