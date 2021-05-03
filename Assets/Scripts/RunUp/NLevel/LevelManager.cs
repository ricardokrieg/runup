using System.Collections.Generic;

namespace RunUp.NLevel {
    public class LevelManager : ILevelManager, ILevelChangeObservable {
        private readonly List<ILevelChangeObserver> _observers;

        private int _currentLevel;
        private ILevelStore _store;
        
        public LevelManager() {
            _observers = new List<ILevelChangeObserver>();
            // TODO program to interface (use Factory)
            _store = new LevelStore();
        }
        
        public void LoadCurrentLevel() {
            LoadLevel(_store.LoadLevel());
        }

        public void NextLevel() {
            LoadLevel(_currentLevel + 1);
        }

        public void SubscribeToLevelChange(ILevelChangeObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }
        
        private void LoadLevel(int level) {
            var previousLevel = _currentLevel;
            _currentLevel = NormalizeLevel(level);
            
            _store.SaveLevel(_currentLevel);
            
            NotifyObservers(previousLevel, _currentLevel);
        }

        private void NotifyObservers(int previousLevel, int nextLevel) {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnLevelChange(previousLevel, nextLevel);
                }
            }
        }

        private static int NormalizeLevel(int level) {
            return level > 2 ? 1 : level;
        }
    }
}