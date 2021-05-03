using UnityEngine;
using UnityEngine.UI;

namespace RunUp.UI {
    public class SoundSlider : MonoBehaviour {
        private NAudio.IAudioSettings _audioSettings;

        void Start() {
            _audioSettings = Container.Instance.Get<NAudio.IAudioSettings>();
            
            Debug.Log("[SoundSlider] Start soundValue = " + _audioSettings.SoundValue());
            
            var slider = GetComponent<Slider>();

            slider.onValueChanged.AddListener(OnValueChanged);
            slider.value = _audioSettings.SoundValue();
        }

        private void OnValueChanged(float value) {
            _audioSettings.SetSoundValue(value);
        }
    }
}
