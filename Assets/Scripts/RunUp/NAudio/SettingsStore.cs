using UnityEngine;

namespace RunUp.NAudio {
    public class SettingsStore : ISettingsStore {
        private const string SoundOffKey = "SoundOff";
        private const string SoundValueKey = "SoundValue";
        
        public bool GetSoundOff() {
            return PlayerPrefs.GetInt(SoundOffKey, 0) == 1;
        }

        public void SetSoundOff(bool value) {
            PlayerPrefs.SetInt(SoundOffKey, value ? 1 : 0);
            Save();
        }

        public float GetSoundValue() {
            return PlayerPrefs.GetFloat(SoundValueKey, 0.5f);
        }

        public void SetSoundValue(float value) {
            PlayerPrefs.SetFloat(SoundValueKey, value);
            Save();
        }

        public void Save() {
            PlayerPrefs.Save();
        }
    }
}