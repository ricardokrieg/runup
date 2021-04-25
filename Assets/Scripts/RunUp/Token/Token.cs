using RunUp.Scene;
using UnityEngine;

namespace RunUp.Token {
    public class Token : MonoBehaviour {
        [SerializeField] private bool final;
        [SerializeField] private string nextLevel;
        
        public void Collect() {
            Debug.Log(final ? "final token collected" : "token collected");

            var position = gameObject.transform.position;
            var rotation = new Quaternion(0, 90, 0, 0);
            
            Destroy(gameObject);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartStream");
            var animationGameObject = Instantiate(animationPrefab, position, rotation);
            Destroy(animationGameObject, 1f);

            if (final && nextLevel != "") {
                var sceneLoader = FindObjectOfType<SceneLoader>();
                sceneLoader.LoadScene(nextLevel);
            }
        }
    }   
}
