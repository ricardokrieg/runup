using UnityEngine;
using Zenject;

namespace RunUp.Audio {
    public class AudioService : ScriptableObject, IInitializable {
        private AudioSource _audioSource;
        
        public void Initialize() {
            Debug.Log("[AudioService] Initialize");
            
            var mainThemeAudioClip = Resources.Load<AudioClip>("Sounds/Casual Title PIANO LOOP na Casual Game Music");

            _audioSource = FindObjectOfType<AudioSource>();
            _audioSource.clip = mainThemeAudioClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        
        public void SetSoundOn() {
            Debug.Log("[AudioService] SetSoundOn");
            
            _audioSource.mute = false;
        }

        public void SetSoundOff() {
            Debug.Log("[AudioService] SetSoundOff");
            
            _audioSource.mute = true;
        }

        public void SetSoundValue(float value) {
            Debug.Log("[AudioService] SetSoundValue " + value);
            
            _audioSource.volume = value;
        }
    }
}
