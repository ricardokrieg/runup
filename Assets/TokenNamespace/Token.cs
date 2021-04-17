using UnityEngine;
using UnityEngine.SceneManagement;

namespace TokenNamespace {
    public class Token : MonoBehaviour {
        [SerializeField] private bool final;
        [SerializeField] private string nextLevel;
    
        public void Collect() {
            Debug.Log(final ? "final token collected" : "token collected");

            Destroy(gameObject);

            if (final && nextLevel != "") {
                SceneManager.LoadScene(nextLevel);    
            }
        }
    }   
}
