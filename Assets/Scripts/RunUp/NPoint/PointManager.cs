using UnityEngine;

namespace RunUp.NPoint {
    public class PointManager : IPointManager {
        private static PointManager instance;

        private int _points;
        private IPointStore _store;
        
        public static PointManager Instance {
            get { return instance ??= new PointManager(); }
        }

        private PointManager() {
            // TODO program to interface (use Factory)
            _store = new PointStore();
            _points = _store.LoadPoints();
            
            Debug.Log("[PointManager] Start Points " + _points);
        }

        public void AddPoints(int points) {
            Debug.Log("[PointManager] AddPoints " + points);

            _points += points;
            
            Debug.Log("[PointManager] Total=" + _points);
            SavePoints();
        }

        private void SavePoints() {
            Debug.Log("[PointManager] SavePoints");
            
            _store.SavePoints(_points);
        }

        private void LoadPoints() {
            Debug.Log("[PointManager] LoadPoints");

            _points = _store.LoadPoints();
        }
    }
}