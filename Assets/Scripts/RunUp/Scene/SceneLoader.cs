using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.Scene {
    public class SceneLoader : MonoBehaviour {
        private List<ISceneLoadObserver> _observers;
        private SceneStore _sceneStore;
        
        private bool _loadingScene;

        public void Start() {
            _sceneStore = new SceneStore();
            _sceneStore.ListSceneNames();
            
            _observers = new List<ISceneLoadObserver>();
        }

        public void LoadScene(string sceneName) {
            Debug.Log("[SceneLoader] Load Scene " + sceneName);
            
            if (_loadingScene) return;

            _sceneStore.AddScene(sceneName);
            if (_sceneStore.SceneIsLoaded(sceneName)) {
                NotifyObservers();
                return;
            }
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void Subscribe(ISceneLoadObserver observer) {
            if (!_observers.Contains(observer)) {
                _observers.Add(observer);
            }
        }

        private IEnumerator LoadSceneAsync(string sceneName) {
            _loadingScene = true;
            
            Debug.Log("[SceneLoader] Unloading Scenes");
            foreach (var loadedSceneName in _sceneStore.GetSceneNames()) {
                if (loadedSceneName == sceneName) continue;
                
                Debug.Log("[SceneLoader] Unloading Scene async " + loadedSceneName);
                var asyncUnload = SceneManager.UnloadSceneAsync(loadedSceneName);

                while (!asyncUnload.isDone) {
                    yield return null;
                }
            }
            Debug.Log("[SceneLoader] Unloaded all Scenes");

            Debug.Log("[SceneLoader] Loading Scene async " + sceneName);
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            Debug.Log("[SceneLoader] Done");
            _sceneStore.ListSceneNames();
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