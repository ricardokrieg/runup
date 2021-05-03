using UnityEngine;

namespace RunUp.NInitializer {
    public class Initializer : MonoBehaviour {
        public void Start() {
            Debug.Log("[Initializer] Start");

            var initializables = Container.Instance.GetInitializables();
            // TODO iterator
            foreach (var initializable in initializables){
                initializable.Initialize();
            }
        }
    }
}