using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class StartArea : MonoBehaviour {
        private GameManager _gameManager;
        
        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }
        
        void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            gameObject.GetComponentInParent<Canvas>().enabled = false;
            _gameManager.Start();
        }
    }
}
