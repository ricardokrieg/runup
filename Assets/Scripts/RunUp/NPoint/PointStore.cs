using CI.QuickSave;
using UnityEngine;

namespace RunUp.NPoint {
    public class PointStore : IPointStore {
        private const string Root = "RunUp";
        private const string Key = "Points";
        
        public int LoadPoints() {
            var points = 0;
            
            try {
                var reader = QuickSaveReader.Create(Root);
                points = reader.Read<int>(Key);
                
                Debug.Log("[PointStore] LoadPoints loaded " + points);
            } catch (QuickSaveException e) {
                Debug.LogWarning(e);
            }

            return points;
        }

        public void SavePoints(int points) {
            Debug.Log("[PointStore] SavePoints " + points);
            
            var writer = QuickSaveWriter.Create(Root);
            writer.Write(Key, points);
            writer.Commit();
        }
    }
}