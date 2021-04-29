using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class RestartButton : MonoBehaviour {
        private GameManager _gameManager;
        
        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }
        
        public void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Debug.Log("[RestartButton] OnClick");
            
            _gameManager.RestartLevel();
        }
    }
}