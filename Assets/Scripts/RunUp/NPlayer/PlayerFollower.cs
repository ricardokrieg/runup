using System;
using UnityEngine;

namespace RunUp.NPlayer {
    public class PlayerFollower : MonoBehaviour {
        // TODO program to interface
        private Player _player;
        private bool _follow;

        // TODO program to interface
        public void Initialize(Player player) {
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
            var currentPosition = transform.position;
            
            return new Vector3(currentPosition.x, _player.transform.position.y, currentPosition.z);
        }
    }    
}
