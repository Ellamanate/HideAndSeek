using HideAndSeek.AI;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyModel : CharacterModel
    {
        public OrderActionType ActionTypes;
        public OrderCounterType CounterTypes;
        public Vector3 Destination;
        public LayerMask RaycastLayers;
        public float VisionDistance;
        public float RepathTime;
        public bool Active;

        public EnemyModel(EnemyConfig config, Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
            Speed = config.Speed;
            ActionTypes = config.Actions;
            CounterTypes = config.Counters;
            RaycastLayers = config.RaycastLayers;
            VisionDistance = config.VisionDistance;
            RepathTime = config.RepathTime;
            Active = true;
        }

        public EnemyModel(EnemyModel anotherModel)
        {
            CopyFrom(anotherModel);
        }

        public void CopyFrom(EnemyModel anotherModel)
        {
            Position = anotherModel.Position;
            Rotation = anotherModel.Rotation;
            Speed = anotherModel.Speed;
            Destroyed = anotherModel.Destroyed;
            Destination = anotherModel.Destination;
            RaycastLayers = anotherModel.RaycastLayers;
            VisionDistance = anotherModel.VisionDistance;
            RepathTime = anotherModel.RepathTime;
            Active = anotherModel.Active;
        }
    }
}
