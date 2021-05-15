using System.Collections;
using System.Collections.Generic;
using RunUp.NObstacle;
using RunUp.NToken;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.NPlayerStatus {
    public class PlayerStatus : IPlayerStatusObservable, ICollisionObserver, ICollectionObserver {
        private NPoint.IPointManager _pointManager;
        private List<IPlayerStatusObserver> _winObservers;
        private List<IPlayerStatusObserver> _lossObservers;

        public PlayerStatus() {
            _pointManager = Container.Instance.Get<NPoint.IPointManager>();
            
            _winObservers = new List<IPlayerStatusObserver>();
            _lossObservers = new List<IPlayerStatusObserver>();
        }

        public void SubscribeToWin(IPlayerStatusObserver observer) {
            if (_winObservers.Contains(observer)) return;
            
            _winObservers.Add(observer);
        }
        
        public void SubscribeToLoss(IPlayerStatusObserver observer) {
            if (_lossObservers.Contains(observer)) return;
            
            _lossObservers.Add(observer);
        }
        
        public void OnCollision() {
            NotifyLossObservers();
        }

        public void OnCollection(Vector2 position, bool isFinal) {
            Debug.Log("[PlayerStatus] OnCollection " + position + " " + isFinal);

            _pointManager.AddPoints(isFinal ? 20 : 1);
            
            if (isFinal) {
                NotifyWinObservers();
            }
        }
        
        private void NotifyWinObservers() {
            foreach (var observer in _winObservers.ToArray()) {
                if (_winObservers.Contains(observer)) {
                    observer.OnWin();
                }
            }
        }
        
        private void NotifyLossObservers() {
            foreach (var observer in _lossObservers.ToArray()) {
                if (_lossObservers.Contains(observer)) {
                    observer.OnLoss();
                }
            }
        }
    }
}