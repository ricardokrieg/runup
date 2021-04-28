using System.Collections.Generic;
using CI.QuickSave;
using UnityEngine;

namespace RunUp.Level {
    public class LevelManager : ILevelManager {
        private readonly List<ILevelChangeObserver> _levelChangeObservers;

        private int _currentLevel;
        
        public LevelManager() {
            _levelChangeObservers = new List<ILevelChangeObserver>();
        }
        
        public void LoadCurrentLevel() {
            LoadLevel(LoadSavedLevel());
        }

        public void NextLevel() {
            LoadLevel(_currentLevel + 1);
        }

        public void SubscribeToLevelChange(ILevelChangeObserver observer) {
            if (_levelChangeObservers.Contains(observer)) return;
            
            _levelChangeObservers.Add(observer);
        }
        
        private void LoadLevel(int level) {
            var previousLevel = _currentLevel;
            _currentLevel = NormalizeLevel(level);
            
            SaveLevel();
            
            NotifyLevelChangeObservers(previousLevel, _currentLevel);
        }

        private void NotifyLevelChangeObservers(int previousLevel, int nextLevel) {
            foreach (var observer in _levelChangeObservers.ToArray()) {
                if (_levelChangeObservers.Contains(observer)) {
                    observer.OnLevelChange(previousLevel, nextLevel);
                }
            }
        }

        private int LoadSavedLevel() {
            var level = 1;
            
            try {
                var reader = QuickSaveReader.Create("RunUp");
                level = reader.Read<int>("CurrentLevel");
                
                Debug.Log("[LevelManager] LoadSavedLevel loaded " + level);
            } catch (QuickSaveException e) {
                Debug.LogWarning(e);
            }

            return level;
        }

        private void SaveLevel() {
            Debug.Log("[LevelManager] SaveLevel " + _currentLevel);
            
            var writer = QuickSaveWriter.Create("RunUp");
            writer.Write("CurrentLevel", _currentLevel);
            writer.Commit();
        }

        private static int NormalizeLevel(int level) {
            return level > 2 ? 1 : level;
        }
    }
}