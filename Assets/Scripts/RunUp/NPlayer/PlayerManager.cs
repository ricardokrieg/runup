using Cinemachine;
using Dreamteck.Splines;
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

        public float PlayerSpeed() {
            return 2f;
        }
        
        public void SpawnPlayer(bool playAnimation = true) {
            Debug.Log("[PlayerManager] SpawnPlayer");

            var playerGameObject = Instantiate(playerPrefab);
            // TODO program to interface
            _player = playerGameObject.GetComponent<Player>();
            _player.GetComponent<SplineFollower>().followSpeed = PlayerSpeed();
            
            _player.gameObject.SetActive(false);
            
            StartPlayer(playAnimation);
        }

        public void RespawnPlayer() {
            Debug.Log("[PlayerManager] RespawnPlayer");
            
            SpawnPlayer(false);
        }

        public void StartPlayer(bool playAnimation = false) {
            Debug.Log("[PlayerManager] StartPlayer");

            var virtualCamera = GameObject.Find("vcam1");
            var virtualCameraComponent = virtualCamera.gameObject.GetComponent<CinemachineVirtualCamera>();
            virtualCameraComponent.Follow = _player.transform;

            PlacePlayer(playAnimation);
        }
        
        private void PlacePlayer(bool playAnimation) {
            var position = _player.transform.position;

            if (playAnimation) {
                Instantiate(spawnAnimationPrefab, position, Quaternion.identity);
            }

            _player.gameObject.SetActive(true);
            _player.Initialize();
        }
    }
}