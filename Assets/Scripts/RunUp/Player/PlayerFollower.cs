using System;
using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class PlayerFollower : MonoBehaviour {
        private Player _player;
        private bool _follow = false;

        [Inject]
        public void Init(Player player) {
            _player = player;
        }
        
        private void LateUpdate() {
            if (_follow) {
                transform.position = Position();    
            } else {
                if (_player.transform.position.y >= transform.position.y) {
                    _follow = true;
                }
            }
        }

        private Vector3 Position() {
            return new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        }
    }    
}
