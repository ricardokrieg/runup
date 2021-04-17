using UnityEngine;

using PlayerNamespace;

namespace CameraNamespace {
    public class Follower : MonoBehaviour {
        [SerializeField] private PlayerManager playerManager;

        private Player _player;
    
        private void Start() {
            _player = playerManager.Player();
        }
    
        private void Update() {
            transform.position = Position();
        }

        private Vector3 Position() {
            return new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }    
}
