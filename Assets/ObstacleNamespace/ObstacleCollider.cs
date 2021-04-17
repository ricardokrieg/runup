using PlayerNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObstacleNamespace {
    public class ObstacleCollider : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.gameObject.CompareTag("Obstacle")) return;
            
            Debug.Log("Collided with obstacle");
            var player = GetComponent<Player>();
            player.PlayerManager().Restart();
        }
    }   
}
