using UnityEngine;
using UnityEngine.UI;
using Zenject;
using AudioSettings = RunUp.Audio.AudioSettings;

namespace RunUp.UI {
    public class SoundSlider : MonoBehaviour {
        private AudioSettings _audioSettings;

        [Inject]
        public void Init(AudioSettings audioSettings) {
            _audioSettings = audioSettings;
        }
        
        void Start() {
            var slider = GetComponent<Slider>();

            slider.onValueChanged.AddListener(OnValueChanged);
            slider.value = _audioSettings.SoundValue();
        }

        private void OnValueChanged(float value) {
            _audioSettings.SetSoundValue(value);
        }
    }
}
