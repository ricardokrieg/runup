using RunUp.NObstacle;
using RunUp.NToken;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.NPlayerStatus {
    public class PlayerStatus : ICollisionObserver, ICollectionObserver {
        public void OnCollision() {
            SceneManager.LoadSceneAsync("Loss", LoadSceneMode.Single);
        }

        public void OnCollection(Vector2 position, bool isFinal) {
            Debug.Log("[PlayerStatus] OnCollection " + position + " " + isFinal);
        }
    }
}