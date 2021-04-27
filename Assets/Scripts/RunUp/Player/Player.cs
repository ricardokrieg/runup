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
            
            // _splineFollower.SetPercent(_splineFollower.spline.Project(_splineFollower.transform.position).percent);
            var startPosition = splineComputer.EvaluatePosition(0);

            // transform.position = startPosition;
            // transform.rotation = new Quaternion(0.5f, 0, 0, 0);
            // Restart();
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
            _splineFollower.Restart();
        }
    } 
}
