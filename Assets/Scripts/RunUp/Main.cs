using System.Linq;
using UnityEngine;

using RunUp.NObstacle;
using RunUp.NPlayerStatus;

namespace RunUp {
    public class Main : MonoBehaviour {
        private PlayerStatus _playerStatus;

        public void Start() {
            Debug.Log("[Main] Start");
            
            Container.Instance.Bind<GameManager>(GameManager.Instance);
            Container.Instance.Bind<NAudio.IAudioService>(NAudio.AudioService.Instance);
            Container.Instance.Bind<NAudio.IAudioSettings>(NAudio.AudioSettings.Instance);
            
            Container.Instance.BindInitializable(NAudio.AudioSettings.Instance);
            
            DontDestroyOnLoad(gameObject);

            InitializePlayerStatus();
        }

        private void InitializePlayerStatus() {
            Debug.Log("[Main] InitializePlayerStatus");
            
            _playerStatus = new PlayerStatus();
            
            var observables = FindObjectsOfType<MonoBehaviour>().OfType<ICollisionObservable>();
            
            foreach (var observable in observables) {
                observable.SubscribeToCollision(_playerStatus);
            }
        }
    }
}