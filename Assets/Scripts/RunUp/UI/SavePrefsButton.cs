using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class SavePrefsButton : MonoBehaviour {
        private Audio.AudioSettings _audioSettings;

        [Inject]
        public void Init(Audio.AudioSettings audioSettings) {
            _audioSettings = audioSettings;
        }
        
        public void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        
        private void OnClick() {
            _audioSettings.Save();
        }
    }
}