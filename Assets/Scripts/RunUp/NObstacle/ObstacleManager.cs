using RunUp.NPlayer;
using UnityEngine;

namespace RunUp.NObstacle {
    public class ObstacleManager : MonoBehaviour {
        public void Start() {
            var playerSpeed = PlayerSpeed();
            
            // TODO program to interface (Obstacle is a concrete class)
            var obstacles = FindObjectsOfType<Obstacle>();

            foreach (var obstacle in obstacles) {
                var animator = obstacle.GetComponent<Animator>();
                animator.speed = obstacle.GetSpeedMultiplier() * playerSpeed;
                
                Debug.Log("[ObstacleManager] Obstacle Speed = " + animator.speed);
            }
        }

        private float PlayerSpeed() {
            var playerManager = Container.Instance.Get<PlayerManager>();

            Debug.Log("[ObstacleManager] Player Speed = " + playerManager.PlayerSpeed());

            return playerManager.PlayerSpeed();
        }
    }
}