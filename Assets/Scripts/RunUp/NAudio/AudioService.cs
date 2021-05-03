using UnityEngine;

namespace RunUp.NAudio {
    public class AudioService : MonoBehaviour, IAudioService {
        [SerializeField] private AudioClip mainThemeAudioClip;
        
        private static AudioService instance;
        public static AudioService Instance => instance;
        
        private AudioSource _audioSource;

        public void Awake() {
            Debug.Log("[AudioService] Awake");
            
            if (instance != null && instance != this) {
                Destroy(gameObject);
            } else {
                instance = this;
            }
        }

        public void Start() {
            Debug.Log("[AudioService] Start");
            
            DontDestroyOnLoad(gameObject);
            
            _audioSource = GetComponent<AudioSource>();
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
