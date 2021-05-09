using UnityEngine;

namespace RunUp {
    public class Main : MonoBehaviour {
        public void Start() {
            Debug.Log("[Main] Start");
            
            Container.Instance.Bind(GameManager.Instance);
            Container.Instance.Bind<NAudio.IAudioService>(NAudio.AudioService.Instance);
            Container.Instance.Bind<NAudio.IAudioSettings>(NAudio.AudioSettings.Instance);
            Container.Instance.Bind<NLevel.ILevelManager>(NLevel.LevelManager.Instance);
            Container.Instance.Bind<NLevel.ILevelChangeObservable>(NLevel.LevelManager.Instance);
            Container.Instance.Bind(NPlayer.PlayerManager.Instance);
            Container.Instance.Bind(Camera.main);
            
            Container.Instance.BindInitializable(NAudio.AudioSettings.Instance);
            
            DontDestroyOnLoad(gameObject);
        }
    }
}