using System.Collections;
using UnityEngine;

namespace RunUp.NPlayer {
    public class PlayerManager : MonoBehaviour {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject spawnAnimationPrefab;
        
        private static PlayerManager instance;
        public static PlayerManager Instance => instance;
        
        // TODO program to interface
        private Player _player;
        
        public void Awake() {
            Debug.Log("[PlayerManager] Awake");
            
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }
        
        public void InstantiatePlayer() {
            Debug.Log("[PlayerManager] InstantiatePlayer");

            var playerGameObject = Instantiate(playerPrefab);
            // TODO program to interface
            _player = playerGameObject.GetComponent<Player>();
            
            _player.gameObject.SetActive(false);
            
            StartPlayer(true);
        }

        public void StartPlayer(bool playAnimation = false) {
            Debug.Log("[PlayerManager] StartPlayer");

            var playerCamera = Container.Instance.Get<Camera>();
            var playerFollower = playerCamera.gameObject.AddComponent<PlayerFollower>();
            playerFollower.Initialize(_player);

            PlacePlayer(playAnimation);
        }
        
        private void PlacePlayer(bool playAnimation) {
            var position = _player.transform.position;

            if (playAnimation) {
                Instantiate(spawnAnimationPrefab, position, Quaternion.identity);
            }

            _player.gameObject.SetActive(true);
            _player.Initialize();
            _player.Start();
        }
    }
}