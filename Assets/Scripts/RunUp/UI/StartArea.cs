using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class StartArea : Button {
        private GameManager _gameManager;
        
        [Inject]
        public void Init(GameManager gameManager) {
            _gameManager = gameManager;
        }

        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            
            gameObject.GetComponentInParent<Canvas>().enabled = false;
            _gameManager.Start();
        }
    }
}
