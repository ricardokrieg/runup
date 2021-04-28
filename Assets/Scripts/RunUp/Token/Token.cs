using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class Token : MonoBehaviour, IToken {
        [SerializeField] private bool final;

        private List<ICollectObserver> _collectObservers;
        private GameManager _gameManager;

        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }

        public void Start() {
            // TODO check if after destroying the gameObject, if this array will keep in memory
            _collectObservers = new List<ICollectObserver>();
            
            SubscribeToCollect(_gameManager);
        }
        
        public void Collect() {
            var position = gameObject.transform.position;
            var rotation = new Quaternion(0, 90, 0, 0);
            
            Destroy(gameObject);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartStream");
            var animationGameObject = Instantiate(animationPrefab, position, rotation);
            Destroy(animationGameObject, 1f);

            NotifyCollectObservers();
        }

        public void SubscribeToCollect(ICollectObserver observer) {
            if (_collectObservers.Contains(observer)) return;
            
            _collectObservers.Add(observer);
        }
        
        private void NotifyCollectObservers() {
            var position = gameObject.transform.position;
            
            foreach (var observer in _collectObservers.ToArray()) {
                if (_collectObservers.Contains(observer)) {
                    observer.OnCollect(position, final);
                }
            }
        }

        public class Factory : PlaceholderFactory<string, Token> {
        }
    }   
}
