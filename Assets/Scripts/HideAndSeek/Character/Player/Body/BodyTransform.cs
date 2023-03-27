using System;
using UnityEngine;

namespace HideAndSeek
{
    public class BodyTransform : IDisposable
    {
        private readonly PlayerBody _body;

        private bool _disposed;

        public BodyTransform(PlayerBody body)
        {
            _body = body;
        }

        public Transform Parent => _disposed ? null : _body.transform.parent;
        public Vector3 Position => _disposed ? Vector3.zero : _body.transform.position;
        public Vector3 Up => _disposed ? Vector3.zero : _body.transform.up;
        public Vector3 Forward => _disposed ? Vector3.zero : _body.transform.forward;
        public Vector3 Right => _disposed ? Vector3.zero : _body.transform.right;
        public Quaternion Rotation => _disposed ? Quaternion.identity : _body.transform.rotation;

        public void Dispose()
        {
            _disposed = true;
        }
    }
}