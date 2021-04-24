using UnityEngine;

namespace RunUp.Player {
    public class PlayerController : MonoBehaviour {
        private Player _player;

        private void Start() {
            _player = GetComponent<Player>();
        }
        
        private void Update () {
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
                    PlayAnimation(touchPosition);
                    
                    _player.StartMoving();
                    break;
                case TouchPhase.Ended:
                    _player.StopMoving();
                    break;
            }
        }

        private void PlayAnimation(Vector3 touchPosition) {
            Debug.Log("[PlayerController] PlayAnimation " + touchPosition);
            var position = new Vector3(touchPosition.x, touchPosition.y, -2f);
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/SoftBodySlam");
            var animationGameObject = Instantiate(animationPrefab, position, Quaternion.identity);
            
            Destroy(animationGameObject, 1f);
        }
    }    
}
