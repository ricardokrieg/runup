using UnityEngine;
using Dreamteck.Splines;

namespace TokenNamespace {
    public class TokenManager : MonoBehaviour {
        [SerializeField] private Token tokenPrefab;
        [SerializeField] private int quantity = 12;
        [SerializeField] private SplineComputer splineComputer;
    
        public void Start() {
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                var position = splineComputer.EvaluatePosition(i);

                PlaceToken(new Vector2(position.x, position.y));
            }
        }

        private void PlaceToken(Vector2 position) {
            Instantiate(tokenPrefab, position, Quaternion.identity);
        }
    }   
}
