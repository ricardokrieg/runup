using System.Collections.Generic;
using UnityEngine;

namespace RunUp.NToken {
    public class Token : MonoBehaviour, ICollectionObservable {
        [SerializeField] private bool final;
        [SerializeField] private GameObject heartStreamPrefab;

        private List<ICollectionObserver> _observers;

        public void Awake() {
            Debug.Log("[Token] Awake");
            
            _observers = new List<ICollectionObserver>();
        }

        public void SubscribeToCollection(ICollectionObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Player")) return;

            PlayAnimation();
            NotifyObservers();
            
            Destroy(gameObject);
        }
        
        private void NotifyObservers() {
            var position = gameObject.transform.position;
            
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnCollection(position, final);
                }
            }
        }

        private void PlayAnimation() {
            var position = gameObject.transform.position;
            var rotation = new Quaternion(0, 90, 0, 0);
            
            var animationGameObject = Instantiate(heartStreamPrefab, position, rotation);
            
            Destroy(animationGameObject, 1f);
        }
    }   
}
