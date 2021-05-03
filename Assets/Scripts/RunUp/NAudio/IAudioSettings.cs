namespace RunUp.NAudio {
    public interface IAudioSettings {
        public bool IsSoundOn();
        public float SoundValue();
        public void SetSoundOn();
        public void SetSoundOff();
        public void SetSoundValue(float value);
    }
}