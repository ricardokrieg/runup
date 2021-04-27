using RunUp.Scene;
using UnityEngine;

namespace RunUp.Token {
    public class Token : MonoBehaviour, Scene.ISceneLoadObserver {
        [SerializeField] private bool final;
        [SerializeField] private string nextLevel;
        
        public void Collect() {
            var position = gameObject.transform.position;
            var rotation = new Quaternion(0, 90, 0, 0);
            
            Destroy(gameObject);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartStream");
            var animationGameObject = Instantiate(animationPrefab, position, rotation);
            Destroy(animationGameObject, 1f);

            if (final && nextLevel != "") {
                var sceneLoader = FindObjectOfType<SceneLoader>();
                
                sceneLoader.Subscribe(this);
                sceneLoader.LoadScene(nextLevel);
            }
        }

        public void OnCompleted() {
            var playerManager = FindObjectOfType<Player.PlayerManager>();
            playerManager.StartPlayer();
        }
    }   
}
