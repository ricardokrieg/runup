using UnityEngine;
using Dreamteck.Splines;

namespace PlayerNamespace {
    public class PlayerManager : MonoBehaviour {
        [SerializeField] private Player playerPrefab;
        [SerializeField] private SplineComputer splineComputer;
    
        private Player _player;
        private SplineFollower _splineFollower;
    
        private void Start() {
            _player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
            _player.SetPlayerManager(this);
            
            _splineFollower = _player.GetComponent<SplineFollower>();
            _splineFollower.spline = splineComputer;
        }

        public Player Player() {
            return _player;
        }

        public void StartMoving() {
            _splineFollower.follow = true;
        }
        
        public void StopMoving() {
            _splineFollower.follow = false;
        }

        public void Restart() {
            _splineFollower.Restart();
        }
    }
}
