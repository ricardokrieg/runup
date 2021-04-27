using System.Linq;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class Player : MonoBehaviour {
        private SplineFollower _splineFollower;
        
        public void Start() {
            var splineComputer = FindObjectOfType<SplineComputer>();
            
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.spline = splineComputer;
            
            var startPosition = splineComputer.EvaluatePosition(0);
        }

        public void Activate() {
            GetComponent<PlayerController>().enabled = true;
        }

        public void StartMoving() {
            _splineFollower.follow = true;
        }
        
        public void StopMoving() {
            _splineFollower.follow = false;
        }

        public void Restart() {
            if (!_splineFollower) return;
            
            _splineFollower.Restart();
        }
    } 
}
