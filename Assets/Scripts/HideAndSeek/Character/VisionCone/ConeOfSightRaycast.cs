using UnityEngine;

namespace HideAndSeek
{
    public class ConeOfSightRaycast : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private float _fieldOfView = 70;
        [SerializeField] private float _viewDistance = 12.5f;
        [SerializeField] private int _rayCount = 2;
        [SerializeField] private LayerMask _mask;

        private Mesh _mesh;

        private void Start()
        {
            Initialize();
        }

        private void LateUpdate()
        {
            RecalculateCone();
        }

        private void Initialize()
        {
            _mesh = new Mesh();
            _meshFilter.mesh = _mesh;
        }

        private void RecalculateCone()
        {
            float angle = GetAngleFromVector(transform.forward) + _fieldOfView / 2;
            float angleIncrease = _fieldOfView / _rayCount;

            Vector3[] vertices = new Vector3[_rayCount + 2];
            Vector2[] uvs = new Vector2[_rayCount + 2];
            int[] triangles = new int[_rayCount * 3];

            vertices[0] = Vector3.zero;

            int vertexIndex = 1;
            int triangleIndex = 0;

            for (int i = 0; i <= _rayCount; i++)
            {
                Vector3 vertex;
                
                if (Physics.Raycast(transform.position, GetVectorFromAngle(angle), out RaycastHit hitInfo, _viewDistance, _mask))
                {
                    GameLogger.DrawLine(transform.position, transform.position + GetVectorFromAngle(angle) * _viewDistance);
                    vertex = hitInfo.point - transform.position;
                }
                else
                {
                    vertex = GetVectorFromAngle(angle) * _viewDistance;
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;
                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }

            _mesh.vertices = vertices;
            _mesh.uv = uvs;
            _mesh.triangles = triangles;

            _meshFilter.transform.localRotation = Quaternion.Euler(0, -transform.eulerAngles.y, 0);
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            float radians = angle * Mathf.PI / 180f;
            return new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians));
        }

        private float GetAngleFromVector(Vector3 vector)
        {
            vector.Normalize();
            float angle = Mathf.Atan2(vector.z, vector.x) * Mathf.Rad2Deg;

            if (angle < 0)
                angle += 360;

            return angle;
        }
    }
}
