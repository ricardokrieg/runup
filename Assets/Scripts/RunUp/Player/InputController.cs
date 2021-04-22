using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class InputController : MonoBehaviour {
        private Player _player;
        private Canvas _menu;

        [Inject]
        public void Init(Player player, [Inject(Id = "Main Menu")] Canvas menu) {
            _player = player;
            _menu = menu;
        }
        
        private void Update () {
            if (_menu.isActiveAndEnabled) return;
            
            foreach (var touch in Input.touches) {
                HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
            }

            if (Input.touchCount > 0) return;
            
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }

        private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
            switch (touchPhase) {
                case TouchPhase.Began:
                    // GameManager.instance.HideUI();
                    _player.StartMoving();
                    break;
                case TouchPhase.Ended:
                    _player.StopMoving();
                    break;
            }
        }
    }    
}
