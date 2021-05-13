using RunUp.NObstacle;
using RunUp.NToken;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.NPlayerStatus {
    public class PlayerStatus : ICollisionObserver, ICollectionObserver {
        private NPoint.IPointManager _pointManager;

        public PlayerStatus() {
            _pointManager = Container.Instance.Get<NPoint.IPointManager>();
        }
        
        public void OnCollision() {
            SceneManager.LoadSceneAsync("Loss", LoadSceneMode.Single);
        }

        public void OnCollection(Vector2 position, bool isFinal) {
            Debug.Log("[PlayerStatus] OnCollection " + position + " " + isFinal);

            _pointManager.AddPoints(isFinal ? 20 : 1);
        }
    }
}