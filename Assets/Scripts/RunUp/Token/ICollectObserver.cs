using UnityEngine;

namespace RunUp.Token {
    public interface ICollectObserver {
        public void OnCollect(Vector2 position, bool isFinal);
    }
}