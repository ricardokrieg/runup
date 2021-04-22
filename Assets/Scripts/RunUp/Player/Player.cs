using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class Player : MonoBehaviour {
        private SplineFollower _splineFollower;
        
        [Inject]
        public void Init(SplineComputer splineComputer) {
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.spline = splineComputer;
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
