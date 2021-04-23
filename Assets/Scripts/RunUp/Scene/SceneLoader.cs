using System.Collections;
using System.Collections.Generic;
using RunUp.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.Scene {
    public class SceneLoader : MonoBehaviour {
        private bool _loadingScene;
        private List<ISceneLoadObserver> _observers;
        private Player.Player _player;

        public void Start() {
            _observers = new List<ISceneLoadObserver>();
        }
        
        public void LoadScene(string sceneName) {
            if (_loadingScene) return;
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void StartScene() {
            if (_loadingScene) return;

            var playerCamera = FindObjectOfType<Camera>();
            var playerFollower = playerCamera.gameObject.AddComponent<Player.PlayerFollower>();
            playerFollower.player = _player;
            
            _player.Activate();
        }

        public void Subscribe(ISceneLoadObserver observer) {
            if (!_observers.Contains(observer)) {
                _observers.Add(observer);
            }
        }
        
        private IEnumerator LoadSceneAsync(string sceneName) {
            _loadingScene = true;

            Debug.Log("[SceneLoader] Loading scene async");
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            Debug.Log("[SceneLoader] Instantiating Player");
            var playerGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            _player = playerGameObject.GetComponent<Player.Player>();
            
            Debug.Log("[SceneLoader] Done");
            _loadingScene = false;

            NotifyObservers();
        }

        private void NotifyObservers() {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnCompleted();
                }
            }

            _observers.Clear();
        }
    }
}