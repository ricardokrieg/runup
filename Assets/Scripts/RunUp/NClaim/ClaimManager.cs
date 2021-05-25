using System;
using System.Collections;
using RunUp.UI;
using RunUp.NPoint;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.NClaim {
    public class ClaimManager : MonoBehaviour {
        private static ClaimManager instance;
        public static ClaimManager Instance => instance;

        private int _points = 100;
        private long _availableAt = -1;
        private IClaimStore _store;
        
        public void Awake() {
            Debug.Log("[ClaimManager] Awake");
            
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
            
            // TODO program to interface (use Factory)
            _store = new ClaimStore();
        }

        public long SecondsToAvailable() {
            var diff = GetAvailableAt() - DateTimeOffset.Now.ToUnixTimeSeconds();

            return diff <= 0 ? 0 : diff;
        }

        public bool IsAvailable() {
            return SecondsToAvailable() == 0;
        }

        public int AvailablePoints() {
            return _points;
        }
        
        public void ClaimPoints() {
            Debug.Log("[ClaimManager] ClaimPoints");

            FindObjectOfType<Menu>().HideMenu();
            SceneManager.LoadSceneAsync("Claim", LoadSceneMode.Single);

            var pointManager = Container.Instance.Get<IPointManager>();
            pointManager.AddPoints(AvailablePoints());
            
            StartCoroutine(Back());
        }

        private IEnumerator Back() {
            yield return new WaitForSeconds(3.0f);

            var levelManager = Container.Instance.Get<NLevel.ILevelManager>();
            levelManager.ReloadCurrentLevel();
            
            FindObjectOfType<Menu>().ShowMenu();
            _store.Clear();
            _availableAt = -1;
        }

        private long GetAvailableAt() {
            if (_availableAt == -1) {
                _availableAt = _store.LoadAvailableAt();
            }

            return _availableAt;
        }
    }
}