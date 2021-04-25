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

            Debug.Log("[SceneLoader] Loading scene async");
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            Debug.Log("[SceneLoader] Instantiating Player");
            var playerGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/Unicorn"));
            _player = playerGameObject.GetComponent<Player.Player>();
            playerGameObject.SetActive(false);
            
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