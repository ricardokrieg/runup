namespace RunUp.NAudio {
    public interface ISettingsStore {
        public bool GetSoundOff();
        public void SetSoundOff(bool value);
        public float GetSoundValue();
        public void SetSoundValue(float value);
        public void Save();
    }
}