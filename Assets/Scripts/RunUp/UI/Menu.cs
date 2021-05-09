using UnityEngine;

namespace RunUp.UI {
    public class Menu : MonoBehaviour {
        public void Start() {
            DontDestroyOnLoad(gameObject);
        }
    }
}