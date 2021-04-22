namespace RunUp.Audio {
    public class AudioSettings {
        private AudioService _audioService;

        public AudioSettings(AudioService audioService) {
            _audioService = audioService;
        }
        
        public void SetSoundOn() {
            _audioService.SetSoundOn();
        }

        public void SetSoundOff() {
            _audioService.SetSoundOff();
        }

        public void SetSoundValue(float value) {
            _audioService.SetSoundValue(value);
        }
    }
}
