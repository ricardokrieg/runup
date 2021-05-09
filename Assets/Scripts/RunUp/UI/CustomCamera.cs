using UnityEngine;

namespace RunUp.UI {
    public class CustomCamera : MonoBehaviour {
        private static CustomCamera instance;
        
        public void Awake() {
            Debug.Log("[CustomCamera] Awake");
            
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }
        
        public void Start() {
            DontDestroyOnLoad(gameObject);
        }
    }
}