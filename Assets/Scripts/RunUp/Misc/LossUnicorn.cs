using UnityEngine;

namespace RunUp.Misc {
    public class LossUnicorn : MonoBehaviour {
        public void Start() {
            var animator = GetComponent<Animator>();
            animator.SetInteger("animation", 4);
        }
    }
}