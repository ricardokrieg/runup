using System.Collections;
using UnityEngine;
using Zenject;

namespace RunUp.Player {
    public class PlayerManager : MonoBehaviour {
        private Player _player;
        private Player.Factory _playerFactory;
        
        [Inject]
        public void Init(Player.Factory playerFactory) {
            Debug.Log("[PlayerManager] Init");
            _playerFactory = playerFactory;
        }
        
        public void InstantiatePlayer() {
            Debug.Log("[PlayerManager] InstantiatePlayer");

            if (_player == null) {
                _player = _playerFactory.Create("Prefabs/Unicorn");    
            }
            
            _player.gameObject.SetActive(false);
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
            _player.Start();
        }
    }
}