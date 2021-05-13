using Dreamteck.Splines;
using UnityEngine;

namespace RunUp.NPlayer {
    public class Player : MonoBehaviour {
        private const string AnimationKey = "animation";
        private const int AnimationFly = 9;
        private const int AnimationIdle = 1;
        
        private SplineFollower _splineFollower;
        private Animator _animator;
        
        public void Start() {
            Debug.Log("[Player] Start");
            var splineComputer = FindObjectOfType<SplineComputer>();
            
            _splineFollower = GetComponent<SplineFollower>();
            _splineFollower.spline = splineComputer;
            _splineFollower.Restart(); // TODO this is needed?

            _animator = GetComponent<Animator>();
            
            // TODO why I need this? how :follow would be already set here?
            if (_splineFollower.follow) {
                StartMoving();
            } else {
                StopMoving();  
            }
        }

        public void Initialize() {
            // TODO program to interface
            GetComponent<PlayerController>().enabled = true;
        }

        public void StartMoving() {
            _splineFollower.follow = true;
            _animator.SetInteger(AnimationKey, AnimationFly);
        }
        
        public void StopMoving() {
            _splineFollower.follow = false;
            _animator.SetInteger(AnimationKey, AnimationIdle);
        }
    } 
}
