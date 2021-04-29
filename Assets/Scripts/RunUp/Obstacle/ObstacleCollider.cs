using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RunUp.Obstacle {
    public class ObstacleCollider : MonoBehaviour {
        private List<ICollisionObserver> _collisionObservers;
        private GameManager _gameManager;
        
        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }
        
        public void Start() {
            // TODO check if after destroying the gameObject, if this array will keep in memory
            _collisionObservers = new List<ICollisionObserver>();
            
            SubscribeToCollision(_gameManager);
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Obstacle")) return;
            
            NotifyCollisionObservers();
        }
        
        public void SubscribeToCollision(ICollisionObserver observer) {
            if (_collisionObservers.Contains(observer)) return;
            
            _collisionObservers.Add(observer);
        }
        
        private void NotifyCollisionObservers() {
            foreach (var observer in _collisionObservers.ToArray()) {
                if (_collisionObservers.Contains(observer)) {
                    observer.OnCollision();
                }
            }
        }
    }   
}
