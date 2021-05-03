using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.NScene {
    public class SceneLoader : MonoBehaviour {
        private List<ISceneLoadObserver> _observers;
        private ISceneStore _sceneStore;
        private bool _loadingScene;

        public void Initialize() {
            Debug.Log("[SceneLoader] Initialize");
            
            _observers = new List<ISceneLoadObserver>();
            
            // TODO program to interface (use Factory)
            _sceneStore = new SceneStore();
            _sceneStore.ListLoadedScenes();
        }

        public void LoadScene(string sceneName) {
            Debug.Log("[SceneLoader] Load scene " + sceneName);
            
            if (_loadingScene) return;

            _sceneStore.AddScene(sceneName);
            if (_sceneStore.SceneIsLoaded(sceneName)) {
                NotifyObservers(sceneName);
                return;
            }
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void SubscribeToSceneLoad(ISceneLoadObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }

        private IEnumerator LoadSceneAsync(string sceneName) {
            _loadingScene = true;
            
            Debug.Log("[SceneLoader] Unloading scenes");
            foreach (var loadedSceneName in _sceneStore.GetSceneNames()) {
                if (loadedSceneName == sceneName) continue;
                
                Debug.Log("[SceneLoader] Unloading scene async " + loadedSceneName);
                // TODO the SceneStore should handle this?
                var asyncUnload = SceneManager.UnloadSceneAsync(loadedSceneName);

                while (!asyncUnload.isDone) {
                    yield return null;
                }
            }
            _sceneStore.RemoveAllExcept(sceneName);
            Debug.Log("[SceneLoader] Unloaded all scenes");

            Debug.Log("[SceneLoader] Loading scene async " + sceneName);
            // TODO the SceneStore should handle this?
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            Debug.Log("[SceneLoader] Done");
            _sceneStore.ListLoadedScenes();
            _loadingScene = false;

            NotifyObservers(sceneName);
        }

        private void NotifyObservers(string sceneName) {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnSceneLoaded(sceneName);
                }
            }
        }
    }
}
