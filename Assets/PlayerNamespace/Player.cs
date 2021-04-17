using UnityEngine;

namespace PlayerNamespace {
    public class Player : MonoBehaviour {
        private PlayerManager _playerManager;

        public void SetPlayerManager(PlayerManager playerManager) {
            _playerManager = playerManager;
        }
        
        public PlayerManager PlayerManager() {
            return _playerManager;
        }
    }    
}

