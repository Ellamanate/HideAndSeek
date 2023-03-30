using UnityEngine;

namespace HideAndSeek
{
    public class PlayerModel
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float Speed;
        public bool Destroyed;

        public Vector3 Forward => Rotation * Vector3.forward; 
        public Vector3 Right => Rotation * Vector3.right;
        public Vector3 Up => Rotation * Vector3.up;
    }
}
