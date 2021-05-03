using System.Collections;
using UnityEngine;
using Zenject;

namespace RunUp.NPlayer {
    public class PlayerManager : MonoBehaviour {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject spawnAnimationPrefab;
        
        // TODO program to interface
        private Player _player;
        
        public void InstantiatePlayer() {
            Debug.Log("[PlayerManager] InstantiatePlayer");

            if (_player == null) {
                var playerGameObject = Instantiate(playerPrefab);
                // TODO program to interface
                _player = playerGameObject.GetComponent<Player>();
            }
            
            _player.gameObject.SetActive(false);
        }

        public void StartPlayer(bool playAnimation = false) {
            // TODO program to interface OR inject camera somehow
            var playerCamera = FindObjectOfType<Camera>();
            // TODO program to interface OR inject camera somehow
            var playerFollower = playerCamera.gameObject.AddComponent<PlayerFollower>();
            playerFollower.Initialize(_player);
            
            StartCoroutine(PlacePlayer(playAnimation));
        }
        
        private IEnumerator PlacePlayer(bool playAnimation) {
            // TODO why I need to wait 100ms before placing the player?
            yield return new WaitForSeconds(0.1f);
            
            var position = _player.transform.position;

            if (playAnimation) {
                var animationGameObject = Instantiate(spawnAnimationPrefab, position, Quaternion.identity);
                // TODO set the animation to autodestroy (create new prefab before making this change)
                Destroy(animationGameObject, 2f);
            }

            _player.gameObject.SetActive(true);
            _player.Initialize();
            _player.Start();
        }
    }
}