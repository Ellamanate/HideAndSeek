﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace HideAndSeek
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;

        [ShowInInspector, ReadOnly] private float _speed;

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetMovementDirection(Vector3 velocity)
        {
            _body.velocity = velocity * _speed;
        }
    }
}