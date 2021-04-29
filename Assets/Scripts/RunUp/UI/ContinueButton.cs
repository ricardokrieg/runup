using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class ContinueButton : MonoBehaviour {
        private GameManager _gameManager;
        
        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }
        
        public void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Debug.Log("[ContinueButton] OnClick");
            
            _gameManager.Continue();
        }
    }
}