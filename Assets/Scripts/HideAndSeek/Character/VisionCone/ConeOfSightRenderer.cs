/*MIT License

Copyright (c) 2020 Josue Candela Perdomo

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using HideAndSeek.AI;
using UnityEngine;

namespace HideAndSeek
{
    public class ConeOfSightRenderer : MonoBehaviour
    {
        private static readonly int ViewDepthTexturedID = Shader.PropertyToID("_ViewDepthTexture");
        private static readonly int ViewSpaceMatrixID = Shader.PropertyToID("_ViewSpaceMatrix");

        [SerializeField] private Camera _viewCamera;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private Color _color;
        [SerializeField] private Color _nonVisibleColor;
        [SerializeField] private float _viewDistance;
        [SerializeField] private float _viewAngle;
        [SerializeField] private float _angleStrength;
        [SerializeField] private float _viewIntervals;
        [SerializeField] private float _viewIntervalsStep;
        [SerializeField] private float _innerCircleSize;
        [SerializeField] private float _circleStrength;

        private Material _material;
        private RenderTexture _depthTexture;

        public bool Enabled { get; private set; } = true;

        public void Initialize()
        {
            _material = _renderer.material; // This generates a copy of the material
            _renderer.material = _material;
            
            _depthTexture = new RenderTexture(_viewCamera.pixelWidth, _viewCamera.pixelHeight, 24, RenderTextureFormat.Depth);
            _viewCamera.targetTexture = _depthTexture;

            SetProperties();

            _material.SetTexture(ViewDepthTexturedID, _viewCamera.targetTexture);
        }

        private void Update()
        {
            if (Enabled)
            {
                _viewCamera.Render();
                _material.SetMatrix(ViewSpaceMatrixID, _viewCamera.projectionMatrix * _viewCamera.worldToCameraMatrix);
            }
        }

        public void SetActive(bool active)
        {
            Enabled = active;
            _renderer.enabled = active;
        }

        public void SetConeData(ConeData data)
        {
            _color = data.VisionColor;
            _nonVisibleColor = data.NonVisionColor;
            _viewAngle = data.Angle;
            _viewDistance = data.Distance;

            SetProperties();
        }

        private void SetProperties()
        {
            if (_material != null)
            {
                RecalculateScale();

                _viewCamera.farClipPlane = _viewDistance;
                _viewCamera.fieldOfView = _viewAngle;
                _material.SetColor("_Color", _color);
                _material.SetColor("_NonVisibleColor", _nonVisibleColor);
                _material.SetFloat("_ViewAngle", _viewAngle);
                _material.SetFloat("_AngleStrength", _angleStrength);
                _material.SetFloat("_ViewIntervals", _viewIntervals);
                _material.SetFloat("_ViewIntervalsStep", _viewIntervalsStep);
                _material.SetFloat("_InnerCircleSize", _innerCircleSize);
                _material.SetFloat("_CircleStrength", _circleStrength);
            }
        }

        private void RecalculateScale()
        {
            transform.localScale = new Vector3(_viewDistance * 2, transform.localScale.y, _viewDistance * 2);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1f, 0f, 1f));
            Gizmos.DrawWireSphere(Vector3.zero, _viewDistance);
            Gizmos.matrix = Matrix4x4.identity;
        }

        private void OnValidate()
        {
            if (_material != null)
            {
                SetProperties();
            }
        }

#endif
    }
}