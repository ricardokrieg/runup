using System;
using CI.QuickSave;
using UnityEngine;

namespace RunUp.NClaim {
    public class ClaimStore : IClaimStore {
        private const string Root = "RunUp";
        private const string Key = "ClaimAvailableAt";
        
        public long LoadAvailableAt() {
            try {
                var reader = QuickSaveReader.Create(Root);
                var availableAt = reader.Read<long>(Key);
                
                Debug.Log("[ClaimStore] Loaded AvailableAt " + availableAt);

                return availableAt;
            } catch (QuickSaveException e) {
                Debug.LogWarning(e);

                return GenerateAvailableAt();
            }
        }

        public void Clear() {
            GenerateAvailableAt();
        }
        
        private long GenerateAvailableAt() {
            var availableAt = DateTimeOffset.Now.ToUnixTimeSeconds() + 600;
            
            Debug.Log("[ClaimStore] Generate AvailableAt " + availableAt);
            
            var writer = QuickSaveWriter.Create(Root);
            writer.Write(Key, availableAt);
            writer.Commit();

            return availableAt;
        }
    }
}