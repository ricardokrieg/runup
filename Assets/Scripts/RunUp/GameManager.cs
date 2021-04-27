using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameManager : IInitializable, Scene.ISceneLoadObserver {
        private string _currentLevel;
        private Scene.SceneLoader _sceneLoader;
        private Player.PlayerManager _playerManager;
        
        [Inject]
        private void Init(
            Level.ILevelProvider levelProvider,
            Scene.SceneLoader sceneLoader,
            Player.PlayerManager playerManager
        ) {
            _currentLevel = levelProvider.CurrentLevel();
            _sceneLoader = sceneLoader;
            _playerManager = playerManager;
        }

        public void Initialize() {
            Debug.Log("[GameManager] Loading scene " + _currentLevel);
            _sceneLoader.Subscribe(this);
            _sceneLoader.LoadScene(_currentLevel);
        }

        public void OnCompleted() {
            Debug.Log("[GameManager] Scene loaded");
            
            _playerManager.InstantiatePlayer();
        }
        
        public void StartGame() {
            _playerManager.StartPlayer(true);
        }
    }
}