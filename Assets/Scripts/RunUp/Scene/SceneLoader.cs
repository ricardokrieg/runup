using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.Scene {
    public class SceneLoader : MonoBehaviour {
        private List<ISceneLoadObserver> _sceneLoadObservers;
        private SceneStore _sceneStore;
        
        private bool _loadingScene;

        public void Initialize() {
            Debug.Log("[SceneLoader] Initialize");
            
            _sceneLoadObservers = new List<ISceneLoadObserver>();
            
            _sceneStore = new SceneStore();
            _sceneStore.ListLoadedScenes();
        }

        public void LoadScene(string sceneName) {
            Debug.Log("[SceneLoader] Load scene " + sceneName);
            
            if (_loadingScene) return;

            _sceneStore.AddScene(sceneName);
            if (_sceneStore.SceneIsLoaded(sceneName)) {
                NotifySceneLoadObservers(sceneName);
                return;
            }
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void SubscribeToSceneLoad(ISceneLoadObserver observer) {
            if (_sceneLoadObservers.Contains(observer)) return;
            
            _sceneLoadObservers.Add(observer);
        }

        private IEnumerator LoadSceneAsync(string sceneName) {
            _loadingScene = true;
            
            Debug.Log("[SceneLoader] Unloading scenes");
            foreach (var loadedSceneName in _sceneStore.GetSceneNames()) {
                if (loadedSceneName == sceneName) continue;
                
                Debug.Log("[SceneLoader] Unloading scene async " + loadedSceneName);
                var asyncUnload = SceneManager.UnloadSceneAsync(loadedSceneName);

                while (!asyncUnload.isDone) {
                    yield return null;
                }
            }
            _sceneStore.RemoveAllExcept(sceneName);
            Debug.Log("[SceneLoader] Unloaded all scenes");

            Debug.Log("[SceneLoader] Loading scene async " + sceneName);
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            Debug.Log("[SceneLoader] Done");
            _sceneStore.ListLoadedScenes();
            _loadingScene = false;

            NotifySceneLoadObservers(sceneName);
        }

        private void NotifySceneLoadObservers(string sceneName) {
            foreach (var observer in _sceneLoadObservers.ToArray()) {
                if (_sceneLoadObservers.Contains(observer)) {
                    observer.OnSceneLoaded(sceneName);
                }
            }
        }
    }
}