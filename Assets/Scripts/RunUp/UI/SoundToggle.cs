using UnityEngine;
using UnityEngine.UI;
using Zenject;
using AudioSettings = RunUp.Audio.AudioSettings;

namespace RunUp.UI {
    public class SoundToggle : MonoBehaviour {
        private AudioSettings _audioSettings;

        [Inject]
        public void Init(AudioSettings audioSettings) {
            _audioSettings = audioSettings;
        }
        
        void Start() {
            var toggle = GetComponent<Toggle>();

            toggle.onValueChanged.AddListener(OnValueChanged);
            toggle.isOn = _audioSettings.IsSoundOn();
        }

        private void OnValueChanged(bool isOn) {
            if (isOn) {
                _audioSettings.SetSoundOn();
            } else {
                _audioSettings.SetSoundOff();
            }
        }
    }
}
