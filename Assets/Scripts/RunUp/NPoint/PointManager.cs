using System.Collections.Generic;
using UnityEngine;

namespace RunUp.NPoint {
    public class PointManager : IPointManager, IPointObservable {
        private static PointManager instance;

        private int _points;
        private IPointStore _store;
        private List<IPointObserver> _observers;
        
        public static PointManager Instance {
            get { return instance ??= new PointManager(); }
        }

        private PointManager() {
            _observers = new List<IPointObserver>();
            
            // TODO program to interface (use Factory)
            _store = new PointStore();
            LoadPoints();
            
            Debug.Log("[PointManager] Start Points " + _points);
        }

        public void AddPoints(int points) {
            Debug.Log("[PointManager] AddPoints " + points);

            var pointsBefore = _points;
            _points += points;
            
            NotifyObservers(pointsBefore, _points);
            
            Debug.Log("[PointManager] Total=" + _points);
            SavePoints();
        }
        
        public void SubscribeToPoint(IPointObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }

        private void SavePoints() {
            Debug.Log("[PointManager] SavePoints");
            
            _store.SavePoints(_points);
        }

        private void LoadPoints() {
            Debug.Log("[PointManager] LoadPoints");

            _points = _store.LoadPoints();
        }
        
        private void NotifyObservers(int pointsBefore, int points) {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnPoint(pointsBefore, points);
                }
            }
        }
    }
}