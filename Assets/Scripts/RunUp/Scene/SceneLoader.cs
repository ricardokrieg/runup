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
        private List<string> _sceneNames;

        public void Start() {
            ListSceneNames();
            
            _observers = new List<ISceneLoadObserver>();
            _sceneNames = new List<string>();
        }
        
        public void LoadScene(string sceneName) {
            Debug.Log("[SceneLoader] Load Scene " + sceneName);
            
            if (_loadingScene) return;

            AddScene(sceneName);
            if (SceneIsLoaded(sceneName)) {
                InstantiatePlayer();
                NotifyObservers();
                return;
            }
            
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        public void StartScene() {
            if (_loadingScene) return;

            var playerCamera = FindObjectOfType<Camera>();
            var playerFollower = playerCamera.gameObject.AddComponent<Player.PlayerFollower>();
            playerFollower.player = _player;

            StartCoroutine(PlacePlayer());
        }

        public void Subscribe(ISceneLoadObserver observer) {
            if (!_observers.Contains(observer)) {
                _observers.Add(observer);
            }
        }

        private IEnumerator PlacePlayer() {
            yield return new WaitForSeconds(0.2f);
            
            var position = _player.transform.position;
            
            var animationPrefab = Resources.Load<GameObject>("Prefabs/HeartPoof");
            var animationGameObject = Instantiate(animationPrefab, position, Quaternion.identity);
            Destroy(animationGameObject, 2f);
            
            _player.gameObject.SetActive(true);
            _player.Activate();
        }
        
        private IEnumerator LoadSceneAsync(string sceneName) {
            _loadingScene = true;
            
            Debug.Log("[SceneLoader] Unloading Scenes");
            foreach (var loadedSceneName in _sceneNames.ToArray()) {
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

            InstantiatePlayer();

            Debug.Log("[SceneLoader] Done");
            ListSceneNames();
            _loadingScene = false;

            NotifyObservers();
        }

        private void AddScene(string sceneName) {
            if (_sceneNames.Contains(sceneName)) return;
            
            _sceneNames.Add(sceneName);
        }
        
        private void ListSceneNames() {
            Debug.Log("[SceneLoader] List of scenes:");
            
            for (var i = 0; i < SceneManager.sceneCount; ++i) {
                var scene = SceneManager.GetSceneAt(i);
                var output = scene.name;
                output += scene.isLoaded ? " (Loaded, " : " (Not Loaded, ";
                output += scene.isDirty ? "Dirty, " : "Clean, ";
                output += scene.buildIndex >= 0 ? "in build)" : "NOT in build)";
                
                Debug.Log("[SceneLoader] " + output);
            }
        }

        private bool SceneIsLoaded(string sceneName) {
            for (var i = 0; i < SceneManager.sceneCount; ++i) {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName) return true;
            }

            return false;
        }

        private void NotifyObservers() {
            foreach (var observer in _observers.ToArray()) {
                if (_observers.Contains(observer)) {
                    observer.OnCompleted();
                }
            }

            _observers.Clear();
        }

        private void InstantiatePlayer() {
            Debug.Log("[SceneLoader] Instantiating Player");
            
            var playerGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unicorn"));
            _player = playerGameObject.GetComponent<Player.Player>();
            
            playerGameObject.SetActive(false);
        }
    }
}