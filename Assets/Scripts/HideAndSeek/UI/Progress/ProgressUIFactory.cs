using System;

namespace HideAndSeek
{
    public class ProgressUIFactory : IFactory<ProgressUI, ProgressUIType>
    {
        private readonly ProgressUI _sliderPrefab;
        private readonly ProgressUI _circlePrefab;

        public ProgressUIFactory(ProgressUI sliderPrefab, ProgressUI circlePrefab)
        {
            _sliderPrefab = sliderPrefab;
            _circlePrefab = circlePrefab;
        }

        public ProgressUI Create(ProgressUIType type)
        {
            return type switch
            {
                ProgressUIType.Slider => UnityEngine.Object.Instantiate(_sliderPrefab),
                ProgressUIType.Circle => UnityEngine.Object.Instantiate(_circlePrefab),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
