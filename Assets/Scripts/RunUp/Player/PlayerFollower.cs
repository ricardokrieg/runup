using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class PlayerFollower : MonoBehaviour {
        private Player _player;

        [Inject]
        public void Init(Player player) {
            _player = player;
        }

        private void LateUpdate() {
            transform.position = Position();
        }

        private Vector3 Position() {
            return new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }    
}
