using System.Collections.Generic;
using HideAndSeek.AI;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyTransformModel
    {
        public Vector3 Destination;
        public Vector3 Position;
        public Quaternion Rotation;
        public Quaternion SightRotation;
        public float RepathTime;
        public bool Moved;

        public Vector3 SightForward => SightRotation * Vector3.forward;
        public Vector3 Forward => Rotation * Vector3.forward;
        public Vector3 Right => Rotation * Vector3.right;
        public Vector3 Up => Rotation * Vector3.up;
    }

    public class EnemyModel : CharacterModel
    {
        public Dictionary<AttentivenessType, AttentivenessData> AttentivenessCollection;
        public OrderActionType ActionTypes;
        public OrderCounterType CounterTypes;
        public Vector3 Destination;
        public Quaternion SightRotation;
        public Quaternion SightTargetRotation;
        public LayerMask RaycastLayers;
        public float RepathTime;
        public float AttentivenesDeclineTime;
        public float MaxDistanceToInteractable;
        public float StopChaseDelay;
        public float CatchShelterChance;
        public bool Active;
        public bool Moved;
        public bool PlayerDetectedInShelter;
        public string Id;

        private AttentivenessData _currentAttentivenessData;
        private AttentivenessType _currentAttentiveness;

        public AttentivenessType CurrentAttentiveness
        {
            get => _currentAttentiveness;
            set
            {
                _currentAttentiveness = value;
                AttentivenessCollection.TryGetValue(_currentAttentiveness, out _currentAttentivenessData);
            }
        }

        public EnemyModel(string id, EnemyConfig config, Vector3 position, Quaternion rotation)
        {
            Id = id;

            AttentivenessCollection = new Dictionary<AttentivenessType, AttentivenessData>
            {
                { AttentivenessType.Relax, config.RelaxState.Attentiveness },
                { AttentivenessType.Seaching, config.SearchingState.Attentiveness },
                { AttentivenessType.Chase, config.ChaseState.Attentiveness }
            };

            CurrentAttentiveness = AttentivenessType.Relax;
            Moved = false;
            
            Position = position;
            Rotation = rotation;
            ActionTypes = config.Actions;
            CounterTypes = config.Counters;
            RaycastLayers = config.RaycastLayers;
            RepathTime = config.RepathTime;
            AttentivenesDeclineTime = config.AttentivenesDeclineTime;
            MaxDistanceToInteractable = config.MaxDistanceToInteractable;
            StopChaseDelay = config.StopChaseDelay;
            CatchShelterChance = config.CatchShelterChance;
        }

        public EnemyModel(EnemyModel anotherModel)
        {
            CopyFrom(anotherModel);
        }

        public float Speed => _currentAttentivenessData.Speed;
        public float VisionDistance => _currentAttentivenessData.VisionDistance;
        public float VisionAngle => _currentAttentivenessData.VisionAngle;
        public float TimeToDetect => _currentAttentivenessData.TimeToDetect;
        public ConeData ConeData => _currentAttentivenessData.ConeData;
        public Vector3 SightForward => SightRotation * Vector3.forward;
        
        public void CopyFrom(EnemyModel anotherModel)
        {
            Position = anotherModel.Position;
            Rotation = anotherModel.Rotation;
            Destroyed = anotherModel.Destroyed;
            AttentivenessCollection = anotherModel.AttentivenessCollection;
            ActionTypes = anotherModel.ActionTypes;
            CounterTypes = anotherModel.CounterTypes;
            CurrentAttentiveness = anotherModel.CurrentAttentiveness;
            Destination = anotherModel.Destination;
            SightRotation = anotherModel.SightRotation;
            SightTargetRotation = anotherModel.SightTargetRotation;
            RaycastLayers = anotherModel.RaycastLayers;
            RepathTime = anotherModel.RepathTime;
            AttentivenesDeclineTime = anotherModel.AttentivenesDeclineTime;
            MaxDistanceToInteractable = anotherModel.MaxDistanceToInteractable;
            StopChaseDelay = anotherModel.StopChaseDelay;
            CatchShelterChance = anotherModel.CatchShelterChance;
            Active = anotherModel.Active;
            Moved = anotherModel.Moved;
            PlayerDetectedInShelter = anotherModel.PlayerDetectedInShelter;
            Id = anotherModel.Id;
        }
    }
}
