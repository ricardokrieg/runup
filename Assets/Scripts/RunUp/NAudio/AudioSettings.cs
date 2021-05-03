using UnityEngine;

namespace RunUp.NAudio {
    public class AudioSettings : IAudioSettings, NInitializer.IInitializable {
        private static AudioSettings instance;

        private readonly IAudioService _audioService;
        private readonly ISettingsStore _store;

        public static AudioSettings Instance {
            get { return instance ??= new AudioSettings(); }
        }

        private AudioSettings() {
            _audioService = Container.Instance.Get<IAudioService>();
            // TODO program to interface (use Factory)
            _store = new SettingsStore();
        }
        
        public void Initialize() {
            Debug.Log("[AudioSettings] Initialize");
            
            if (!IsSoundOn()) {
                _audioService.SetSoundOff();    
            }
            _audioService.SetSoundValue(SoundValue());
        }

        public bool IsSoundOn() {
            return !_store.GetSoundOff();
        }

        public float SoundValue() {
            return _store.GetSoundValue();
        }

        public void SetSoundOn() {
            _audioService.SetSoundOn();
            _store.SetSoundOff(false);
        }

        public void SetSoundOff() {
            _audioService.SetSoundOff();
            _store.SetSoundOff(true);
        }

        public void SetSoundValue(float value) {
            _audioService.SetSoundValue(value);
            _store.SetSoundValue(value);
        }
    }
}
