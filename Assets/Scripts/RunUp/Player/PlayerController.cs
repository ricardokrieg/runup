using UnityEngine;

namespace RunUp.Player {
    public class PlayerController : MonoBehaviour {
        private Player _player;

        private void Start() {
            _player = GetComponent<Player>();
        }
        
        private void Update () {
            if (Application.isMobilePlatform) {
                HandleMobile();
            } else {
                HandleDesktop();
            }
        }

        private void HandleMobile() {
            var touched = false;
            
            foreach (var touch in Input.touches) {
                touched = true;
                HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
            }

            if (!touched) {
                _player.StopMoving();
            }
        }

        private void HandleDesktop() {
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            } else if (Input.GetMouseButton(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            } else if (Input.GetMouseButtonUp(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            } else {
                _player.StopMoving();
            }
        }

        private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
            Debug.Log("[PlayerController] HandleTouch " + touchPhase);
            
            switch (touchPhase) {
                case TouchPhase.Began:
                    PlayTouchAnimation(touchPosition);
                    
                    _player.StartMoving();
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    _player.StartMoving();
                    break;
                case TouchPhase.Ended:
                    PlayTouchAnimation(touchPosition);
                    
                    _player.StopMoving();
                    break;
            }
        }

        private void PlayTouchAnimation(Vector3 touchPosition) {
            Debug.Log("[PlayerController] PlayAnimation " + touchPosition);
            var position = new Vector3(touchPosition.x, touchPosition.y, -2f);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/SoftBodySlam");
            var animationGameObject = Instantiate(animationPrefab, position, Quaternion.identity);
            
            Destroy(animationGameObject, 1f);
        }
    }    
}
