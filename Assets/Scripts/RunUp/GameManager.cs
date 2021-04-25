using System;
using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameManager : IInitializable, Scene.ISceneLoadObserver {
        private string _currentLevel;
        private Scene.SceneLoader _sceneLoader;
        
        [Inject]
        private void Init(Level.ILevelProvider levelProvider, Scene.SceneLoader sceneLoader) {
             _currentLevel = levelProvider.CurrentLevel();
             _sceneLoader = sceneLoader;
        }

        public void Initialize() {
            Debug.Log("[GameManager] Loading scene " + _currentLevel);
            _sceneLoader.Subscribe(this);
            _sceneLoader.LoadScene(_currentLevel);
        }

        public void Start() {
            Debug.Log("[GameManager] Start");
            
            // TODO what if this happens before the scene is loaded?
            // should convert to Command pattern?
            _sceneLoader.StartScene();
        }

        public void OnCompleted() {
            Debug.Log("[GameManager] Scene loaded");
        }
    }
}