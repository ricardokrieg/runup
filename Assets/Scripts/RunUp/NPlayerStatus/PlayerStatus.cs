using RunUp.NObstacle;
using RunUp.NToken;
using UnityEngine;

namespace RunUp.NPlayerStatus {
    public class PlayerStatus : ICollisionObserver, ICollectionObserver {
        public void OnCollision() {
            Debug.Log("[PlayerStatus] OnCollision");
        }

        public void OnCollection(Vector2 position, bool isFinal) {
            Debug.Log("[PlayerStatus] OnCollection " + position + " " + isFinal);
        }
    }
}