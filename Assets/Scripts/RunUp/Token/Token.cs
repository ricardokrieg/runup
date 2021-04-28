using RunUp.Scene;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class Token : MonoBehaviour {
        [SerializeField] private bool final;

        private Level.ILevelManager _levelManager;

        [Inject]
        public void Init(Level.ILevelManager levelManager) {
            _levelManager = levelManager;
        }
        
        public void Collect() {
            var position = gameObject.transform.position;
            var rotation = new Quaternion(0, 90, 0, 0);
            
            Destroy(gameObject);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartStream");
            var animationGameObject = Instantiate(animationPrefab, position, rotation);
            Destroy(animationGameObject, 1f);

            if (final) {
                Debug.Log("[Token] Collect final");
                _levelManager.NextLevel();
            }
        }

        public class Factory : PlaceholderFactory<string, Token> {
        }
    }   
}
