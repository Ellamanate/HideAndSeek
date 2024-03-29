﻿using System;
using UnityEngine;

namespace HideAndSeek
{
    public class EnemyBody : MonoBehaviour, IDestroyable
    {
        public event Action OnDestroyed;
        
        [field: SerializeField] public EnemyBodyMovement Movement { get; private set; }
        [field: SerializeField] public InteractableEnemyTrigger InteractableTrigger { get; private set; }
        [field: SerializeField] public PlayerTrigger PlayerTrigger { get; private set; }
        [field: SerializeField] public Transform RaycastPosition { get; private set; }
        [field: SerializeField] public ConeOfSightRenderer VisionCone { get; private set; }
        [field: SerializeField] public float MaxSightRotationSpeed { get; private set; }
        [field: SerializeField] public float MinSightSpeedCorrectio { get; private set; }

        public Quaternion SightRotation => VisionCone.transform.rotation;

        public void SetViewRotation(Quaternion rotation)
        {
            VisionCone.transform.rotation = rotation;
        }

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