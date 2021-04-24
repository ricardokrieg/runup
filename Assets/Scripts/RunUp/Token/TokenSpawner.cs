using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;

        public void Start() {
            var splineComputer = FindObjectOfType<SplineComputer>();
            
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                var position = splineComputer.EvaluatePosition(i);

                PlaceToken(position);
            }
        }

        private void PlaceToken(Vector3 position) {
            // Instantiate(Resources.Load<GameObject>("Prefabs/Token"), position, Quaternion.identity);
            var rotation = new Quaternion(0, 0, 0, 0);
            Instantiate(Resources.Load<GameObject>("Prefabs/HeartToken"), position, Quaternion.identity);
        }
    }   
}
