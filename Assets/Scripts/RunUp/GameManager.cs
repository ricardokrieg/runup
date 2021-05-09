using System.Collections;
using RunUp.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp {
    public class GameManager : MonoBehaviour, NToken.ICollectionObserver, NLevel.ILevelChangeObserver, IEventObserver {
        private static GameManager instance;

        private NPlayer.PlayerManager _playerManager;
        private NLevel.ILevelManager _levelManager;
        private NLevel.ILevelChangeObservable _levelChangeObservable;
        
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

            StartCoroutine(LoadScene("Level " + nextLevel));
            // SceneManager.LoadSceneAsync("Level " + nextLevel, LoadSceneMode.Single);
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

        private IEnumerator LoadScene(string sceneName) {
            Debug.Log("[GameManager] LoadScene " + sceneName);
            
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            while (!asyncOperation.isDone) {
                yield return null;
            }
            
            Debug.Log("[GameManager] Scene loaded");
            
            _playerManager.RespawnPlayer();
        }
        
        // public void Start() {
        //     Debug.Log("[GameManager] Start");
        //     Debug.Log("[GameManager] persistentDataPath: " + Application.persistentDataPath);
        //     
        //     _sceneLoader.Initialize();
        //     
        //     _levelManager.SubscribeToLevelChange(this);
        //     _levelManager.LoadCurrentLevel();
        // }
        //
        // public void OnLevelChange(int previousLevel, int nextLevel) {
        //     Debug.Log("[GameManager] OnLevelChange " + previousLevel + " -> " + nextLevel);
        //     
        //     var level = "Level " + nextLevel;
        //     Debug.Log("[GameManager] Loading scene " + level);
        //     
        //     _sceneLoader.SubscribeToSceneLoad(this);
        //     _sceneLoader.LoadScene(level);
        // }
        //
        // public void OnSceneLoaded(string sceneName) {
        //     Debug.Log("[GameManager] OnSceneLoaded " + sceneName);
        //
        //     if (sceneName == "Win") return;
        //     
        //     if (_gameStarted) {
        //         _playerManager.StartPlayer();
        //     } else {
        //         _playerManager.InstantiatePlayer();
        //     }
        // }
        //
        // public void OnCollect(Vector2 position, bool isFinal) {
        //     Debug.Log("[GameManager] OnCollect " + position + " " + isFinal);
        //
        //     if (isFinal) {
        //         Win();
        //     }
        // }
        //
        // public void OnCollision() {
        //     Debug.Log("[GameManager] OnCollision");
        //     
        //     // var player = GetComponent<Player>();
        //     // player.Start();
        //
        //     Loss();
        // }
        //
        // private void Win() {
        //     _sceneLoader.LoadScene("Win");
        // }
        //
        // private void Loss() {
        //     _sceneLoader.LoadScene("Loss");
        // }
        //
        // public void NextLevel() {
        //     Debug.Log("[GameManager] NextLevel");
        //     
        //     _levelManager.NextLevel();
        // }
        //
        // public void RestartLevel() {
        //     Debug.Log("[GameManager] RestartLevel");
        //     
        //     _levelManager.LoadCurrentLevel();
        // }
    }
}