using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameManager : IInitializable, Scene.ISceneLoadObserver, Level.ILevelChangeObserver, Token.ICollectObserver {
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

        public void Initialize() {
            Debug.Log("[GameManager] Initialize");
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
                // _levelManager.NextLevel();
                Win();
            }
        }

        private void Win() {
            // TODO disable MainCamera (Camera, Audio Listener)
            // TODO disable Main Menu (EventSystem)
            
            _sceneLoader.LoadScene("Win");
        }
    }
}