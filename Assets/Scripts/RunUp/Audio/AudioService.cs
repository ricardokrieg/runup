using UnityEngine;
using Zenject;

namespace RunUp.Audio {
    public class AudioService {
        private readonly AudioSource _audioSource;
        
        public AudioService(
            AudioSource audioSource,
            [Inject(Id = "Main Theme")]
            AudioClip mainThemeAudioClip) {
            _audioSource = audioSource;
            
            _audioSource.clip = mainThemeAudioClip;
            _audioSource.Play();
        }
        
        public void SetSoundOn() {
            _audioSource.mute = false;
        }

        public void SetSoundOff() {
            _audioSource.mute = true;
        }

        public void SetSoundValue(float value) {
            _audioSource.volume = value;
        }
    }
}
