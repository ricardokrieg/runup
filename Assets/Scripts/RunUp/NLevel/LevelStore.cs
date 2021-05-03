using CI.QuickSave;
using UnityEngine;

namespace RunUp.NLevel {
    public class LevelStore : ILevelStore {
        private const string Root = "RunUp";
        private const string Key = "CurrentLevel";
        
        public int LoadLevel() {
            var level = 1;
            
            try {
                var reader = QuickSaveReader.Create(Root);
                level = reader.Read<int>(Key);
                
                Debug.Log("[LevelStore] LoadLevel loaded " + level);
            } catch (QuickSaveException e) {
                Debug.LogWarning(e);
            }

            return level;
        }

        public void SaveLevel(int level) {
            Debug.Log("[LevelStore] SaveLevel " + level);
            
            var writer = QuickSaveWriter.Create(Root);
            writer.Write(Key, level);
            writer.Commit();
        }
    }
}