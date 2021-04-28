using UnityEngine;

namespace RunUp.Misc {
    public class WinUnicorn : MonoBehaviour {
        public void Start() {
            var animator = GetComponent<Animator>();
            animator.SetInteger("animation", 3);
        }
    }
}