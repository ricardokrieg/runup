using System.Linq;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class Player : MonoBehaviour {
        private SplineFollower _splineFollower;
        private Animator _animator;
        
        public void Start() {
            var splineComputer = FindObjectOfType<SplineComputer>();
            
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.spline = splineComputer;

            _animator = GetComponent<Animator>();
            
            if (_splineFollower.follow) {
                StartMoving();
            } else {
                StopMoving();  
            }
        }

        public void Activate() {
            GetComponent<PlayerController>().enabled = true;
        }

        public void StartMoving() {
            _splineFollower.follow = true;
            _animator.SetInteger("animation", 9);
        }
        
        public void StopMoving() {
            _splineFollower.follow = false;
            _animator.SetInteger("animation", 1);
        }

        public void Restart() {
            if (!_splineFollower) return;
            
            _splineFollower.Restart();
        }
    } 
}
