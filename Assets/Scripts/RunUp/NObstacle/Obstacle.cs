using System.Collections.Generic;
using UnityEngine;

namespace RunUp.NObstacle {
    public class Obstacle : MonoBehaviour, ICollisionObservable {
        private List<ICollisionObserver> _observers;
        
        public void Start() {
            _observers = new List<ICollisionObserver>();
        }
        
        public void SubscribeToCollision(ICollisionObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;
            
            NotifyObservers();
        }
        
        private void NotifyObservers() {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnCollision();
                }
            }
        }
    }   
}
