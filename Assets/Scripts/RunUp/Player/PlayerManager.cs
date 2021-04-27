using System.Collections;
using UnityEngine;

namespace RunUp.Player {
    public class PlayerManager : MonoBehaviour {
        private Player _player;
        
        public void InstantiatePlayer() {
            if (FindObjectOfType<Player>()) return;
            
            Debug.Log("[GameManager] Instantiating Player");
            
            var playerGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unicorn"));
            _player = playerGameObject.GetComponent<Player>();
            
            playerGameObject.SetActive(false);
        }

        public void StartPlayer(bool playAnimation = false) {
            var playerCamera = FindObjectOfType<Camera>();
            var playerFollower = playerCamera.gameObject.AddComponent<PlayerFollower>();
            playerFollower.player = _player;
            
            StartCoroutine(PlacePlayer(playAnimation));
        }
        
        private IEnumerator PlacePlayer(bool playAnimation) {
            yield return new WaitForSeconds(0.1f);
            
            var position = _player.transform.position;

            if (playAnimation) {
                var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartPoof");
                var animationGameObject = Instantiate(animationPrefab, position, Quaternion.identity);
                Destroy(animationGameObject, 2f);    
            }

            _player.gameObject.SetActive(true);
            _player.Activate();
            _player.Restart();
        }
    }
}