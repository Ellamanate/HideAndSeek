using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace HideAndSeek
{
    [ShowOdinSerializedPropertiesInInspector]
    public class ProgressUI : BaseWorldUI, ISerializationCallbackReceiver, ISupportsPrefabSerialization
    {
        [OdinSerialize] private IProgressTarget _progressTarget;

        public void SetProgress(float progress)
        {
            _progressTarget.SetProgress(progress);
        }

        [SerializeField, HideInInspector]
        private SerializationData serializationData;

        SerializationData ISupportsPrefabSerialization.SerializationData { get { return this.serializationData; } set { this.serializationData = value; } }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        }
    }

    public interface IProgressTarget
    {
        public void SetProgress(float progress);
    }

    [System.Serializable]
    public class SliderProgress : IProgressTarget
    {
        [SerializeField] private Slider _slider;

        public void SetProgress(float progress)
        {
            _slider.value = progress;
        }
    }

    [System.Serializable]
    public class ImageProgress : IProgressTarget
    {
        [SerializeField] private Image _image;

        public void SetProgress(float progress)
        {
            _image.fillAmount = progress;
        }
    }
}
