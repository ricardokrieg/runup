using RunUp.Misc;
using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameManager : MonoBehaviour, Scene.ISceneLoadObserver, Level.ILevelChangeObserver, Token.ICollectObserver, Obstacle.ICollisionObserver {
        private Level.ILevelManager _levelManager;
        private Scene.SceneLoader _sceneLoader;
        private Player.PlayerManager _playerManager;

        private bool _gameStarted;
        
        [Inject]
        private void Init(
            Level.ILevelManager levelManager,
            Scene.SceneLoader sceneLoader,
            Player.PlayerManager playerManager
        ) {
            _levelManager = levelManager;
            _sceneLoader = sceneLoader;
            _playerManager = playerManager;
        }

        public void Start() {
            Debug.Log("[GameManager] Start");
            Debug.Log("[GameManager] persistentDataPath: " + Application.persistentDataPath);
            
            _sceneLoader.Initialize();
            
            _levelManager.SubscribeToLevelChange(this);
            _levelManager.LoadCurrentLevel();
        }

        public void StartGame() {
            _gameStarted = true;
            
            // TODO use coroutine, because this may be called before player is instantiated
            _playerManager.StartPlayer(true);
        }

        public void OnLevelChange(int previousLevel, int nextLevel) {
            Debug.Log("[GameManager] OnLevelChange " + previousLevel + " -> " + nextLevel);
            
            var level = "Level " + nextLevel;
            Debug.Log("[GameManager] Loading scene " + level);
            
            _sceneLoader.SubscribeToSceneLoad(this);
            _sceneLoader.LoadScene(level);
        }
        
        public void OnSceneLoaded(string sceneName) {
            Debug.Log("[GameManager] OnSceneLoaded " + sceneName);

            if (sceneName == "Win") return;
            
            if (_gameStarted) {
                _playerManager.StartPlayer();
            } else {
                _playerManager.InstantiatePlayer();
            }
        }

        public void OnCollect(Vector2 position, bool isFinal) {
            Debug.Log("[GameManager] OnCollect " + position + " " + isFinal);

            if (isFinal) {
                Win();
            }
        }
        
        public void OnCollision() {
            Debug.Log("[GameManager] OnCollision");
            
            // var player = GetComponent<Player>();
            // player.Start();

            Loss();
        }

        private void Win() {
            _sceneLoader.LoadScene("Win");
        }
        
        private void Loss() {
            _sceneLoader.LoadScene("Loss");
        }
        
        public void NextLevel() {
            Debug.Log("[GameManager] NextLevel");
            
            _levelManager.NextLevel();
        }
        
        public void RestartLevel() {
            Debug.Log("[GameManager] RestartLevel");
            
            _levelManager.LoadCurrentLevel();
        }
    }
}