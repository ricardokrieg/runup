using System;
using UnityEngine;

namespace RunUp.Player {
    public class PlayerFollower : MonoBehaviour {
        public Player player;
        
        private bool _follow;

        private void LateUpdate() {
            if (_follow) {
                transform.position = Position();    
            } else {
                if (player.transform.position.y >= transform.position.y) {
                    _follow = true;
                }
            }
        }

        private Vector3 Position() {
            return new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }    
}
