using System.Collections;
using System.Linq;
using RunUp.NObstacle;
using RunUp.NPlayerStatus;
using RunUp.NToken;
using RunUp.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp {
    public class GameManager : MonoBehaviour, NToken.ICollectionObserver, NLevel.ILevelChangeObserver, IEventObserver {
        private static GameManager instance;

        private NPlayer.PlayerManager _playerManager;
        private NLevel.ILevelManager _levelManager;
        private NLevel.ILevelChangeObservable _levelChangeObservable;
        private PlayerStatus _playerStatus;
        
        public static GameManager Instance => instance;
        
        public void Awake() {
            Debug.Log("[GameManager] Awake");
            
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }

        public void Start() {
            Debug.Log("[GameManager] Start");
            Debug.Log("[GameManager] persistentDataPath: " + Application.persistentDataPath);
            
            _playerManager = Container.Instance.Get<NPlayer.PlayerManager>();
            _levelChangeObservable = Container.Instance.Get<NLevel.ILevelChangeObservable>();
            _levelManager = Container.Instance.Get<NLevel.ILevelManager>();
            
            _levelChangeObservable.SubscribeToLevelChange(this);
            _levelManager.LoadCurrentLevel();
        }
        
        public void OnCollection(Vector2 position, bool isFinal) {
            Debug.Log("[GameManager] OnCollection " + position + " " + isFinal);

            if (isFinal) {
                SceneManager.LoadSceneAsync("Win", LoadSceneMode.Single);
            }
        }
        
        public void OnLevelChange(int previousLevel, int nextLevel) {
            Debug.Log("[GameManager] OnLevelChange " + previousLevel + " -> " + nextLevel);

            StartCoroutine(LoadScene("Level " + nextLevel, previousLevel != 0));
        }
        
        public void OnEvent(UIEvent uiEvent) {
            Debug.Log("[GameManager] OnEvent " + uiEvent.type);

            switch (uiEvent.type) {
                case UIEvent.Type.StartGame:
                    _playerManager.SpawnPlayer();
                    break;
                case UIEvent.Type.NextLevel:
                    _levelManager.NextLevel();
                    break;
            }
        }

        private IEnumerator LoadScene(string sceneName, bool respawnPlayer) {
            Debug.Log("[GameManager] LoadScene " + sceneName);
            
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            while (!asyncOperation.isDone) {
                yield return null;
            }
            
            Debug.Log("[GameManager] Scene loaded");
            
            InitializePlayerStatus();
            if (respawnPlayer) {
                _playerManager.RespawnPlayer();    
            }
        }
        
        private void InitializePlayerStatus() {
            Debug.Log("[GameManager] InitializePlayerStatus");
            
            _playerStatus = new PlayerStatus();
            
            var collisionObservables = FindObjectsOfType<MonoBehaviour>().OfType<ICollisionObservable>();
            
            foreach (var observable in collisionObservables) {
                observable.SubscribeToCollision(_playerStatus);
            }
            
            var collectionObservables = FindObjectsOfType<MonoBehaviour>().OfType<ICollectionObservable>();
            
            foreach (var observable in collectionObservables) {
                observable.SubscribeToCollection(_playerStatus);
            }
        }
    }
}