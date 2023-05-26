using Infrastructure;
using System;
using System.Collections.Generic;

namespace HideAndSeek
{
    public class LevelsService
    {
        private readonly Dictionary<LevelData, Level> _levels;
        private readonly LevelsConfig _levelsConfig;

        private int _lastLevelIndex;

        public Level CurrentLevel { get; private set; }

        public LevelsService(LevelsConfig levelsConfig)
        {
            _levels = new Dictionary<LevelData, Level>();
            _levelsConfig = levelsConfig;
        }

        public bool TryGetLevel(LevelData levelData, out Level level)
        {
            return _levels.TryGetValue(levelData, out level);
        }

        public void SelectLevel(LevelData levelData)
        {
            SetLevel(levelData);
            _lastLevelIndex = Array.IndexOf(_levelsConfig.Levels, levelData);
            GameLogger.Log($"SetLevel {levelData.name} index {_lastLevelIndex}");
        }

        public void CompleteLevel()
        {
            CurrentLevel.Complete();

            if (_lastLevelIndex + 1 < _levelsConfig.Levels.Length)
            {
                _lastLevelIndex++;
                var nextLevel = _levelsConfig.Levels[_lastLevelIndex];
                SetLevel(nextLevel);
            }
            else
            {
                GameLogger.Log("Complete all levels");
                // CompleteAll
            }
        }

        private void SetLevel(LevelData levelData)
        {
            if (_levels.TryGetValue(levelData, out var level) == false)
            {
                level = new Level(levelData);
            }

            level.Open();
            CurrentLevel = level;
        }
    }
}
