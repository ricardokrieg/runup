using System.Collections;
using System.Linq;
using RunUp.NObstacle;
using RunUp.NPlayerStatus;
using RunUp.NToken;
using RunUp.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp {
    public class GameManager : MonoBehaviour, NLevel.ILevelChangeObserver, IEventObserver, IPlayerStatusObserver {
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
        
        public void OnLevelChange(int previousLevel, int nextLevel) {
            Debug.Log("[GameManager] OnLevelChange " + previousLevel + " -> " + nextLevel);

            StartCoroutine(LoadLevelScene("Level " + nextLevel, previousLevel != 0));
        }
        
        public void OnEvent(UIEvent uiEvent) {
            Debug.Log("[GameManager] OnEvent " + uiEvent.type);

            switch (uiEvent.type) {
                case UIEvent.Type.StartGame:
                    // TODO program to interface
                    FindObjectOfType<Menu>().HideMenu();
                    FindObjectOfType<Menu>().ShowPointsPanel();
                    _playerManager.SpawnPlayer();
                    break;
                case UIEvent.Type.NextLevel:
                    FindObjectOfType<Menu>().ShowPointsPanel();
                    _levelManager.NextLevel();
                    break;
                case UIEvent.Type.RestartGame:
                    FindObjectOfType<Menu>().ShowPointsPanel();
                    _levelManager.LoadCurrentLevel();
                    break;
            }
        }
        
        public void OnWin() {
            Debug.Log("[GameManager] OnWin");
            
            StartCoroutine(LoadSceneWithDelay("Win"));
        }

        public void OnLoss() {
            Debug.Log("[GameManager] OnLoss");
            
            StartCoroutine(LoadSceneWithDelay("Loss"));
        }

        private IEnumerator LoadLevelScene(string sceneName, bool respawnPlayer) {
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
            
            _playerStatus.SubscribeToWin(this);
            _playerStatus.SubscribeToLoss(this);
            
            var collisionObservables = FindObjectsOfType<MonoBehaviour>().OfType<ICollisionObservable>();
            
            foreach (var observable in collisionObservables) {
                observable.SubscribeToCollision(_playerStatus);
            }
            
            var collectionObservables = FindObjectsOfType<MonoBehaviour>().OfType<ICollectionObservable>();
            
            foreach (var observable in collectionObservables) {
                observable.SubscribeToCollection(_playerStatus);
            }
        }
        
        private IEnumerator LoadSceneWithDelay(string sceneName) {
            yield return new WaitForSeconds(0.5f);
            
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            FindObjectOfType<Menu>().HidePointsPanel();
        }
    }
}