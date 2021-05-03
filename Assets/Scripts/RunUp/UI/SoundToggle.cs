using UnityEngine;
using UnityEngine.UI;

namespace RunUp.UI {
    public class SoundToggle : MonoBehaviour {
        private NAudio.IAudioSettings _audioSettings;

        void Start() {
            _audioSettings = Container.Instance.Get<NAudio.IAudioSettings>();
            
            Debug.Log("[SoundToggle] Start isSoundOn? " + _audioSettings.IsSoundOn());
            
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
