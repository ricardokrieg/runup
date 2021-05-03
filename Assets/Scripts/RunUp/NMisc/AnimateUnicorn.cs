using UnityEngine;

namespace RunUp.NMisc {
    public class AnimateUnicorn : MonoBehaviour {
        [SerializeField] private int animation = 0;
        
        private const string AnimationKey = "animation";
        
        public void Start() {
            var animator = GetComponent<Animator>();
            animator.SetInteger(AnimationKey, animation);
        }
    }
}