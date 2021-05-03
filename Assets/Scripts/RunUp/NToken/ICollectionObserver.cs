using UnityEngine;

namespace RunUp.NToken {
    public interface ICollectionObserver {
        public void OnCollection(Vector2 position, bool isFinal);
    }
}