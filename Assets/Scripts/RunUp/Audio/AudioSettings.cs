using UnityEngine;
using Zenject;

namespace RunUp.Audio {
    public class AudioSettings : IInitializable {
        private AudioService _audioService;

        public AudioSettings(AudioService audioService) {
            _audioService = audioService;
        }

        public void Initialize() {
            if (!IsSoundOn()) {
                _audioService.SetSoundOff();    
            }
            _audioService.SetSoundValue(SoundValue());
        }

        public bool IsSoundOn() {
            return PlayerPrefs.GetInt("SoundOff", 0) == 0;
        }

        public float SoundValue() {
            return PlayerPrefs.GetFloat("SoundValue", 0.5f);
        }

        public void Save() {
            Debug.Log("[AudioSettings] Save");
            
            PlayerPrefs.Save();
        }
        
        public void SetSoundOn() {
            _audioService.SetSoundOn();
            PlayerPrefs.SetInt("SoundOff", 0);
            Save();
        }

        public void SetSoundOff() {
            _audioService.SetSoundOff();
            PlayerPrefs.SetInt("SoundOff", 1);
            Save();
        }

        public void SetSoundValue(float value) {
            _audioService.SetSoundValue(value);
            PlayerPrefs.SetFloat("SoundValue", value);
        }
    }
}
