using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;
        private SplineComputer _splineComputer;

        [Inject]
        public void Init(SplineComputer splineComputer) {
            _splineComputer = splineComputer;
        }

        public void Start() {
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                var position = _splineComputer.EvaluatePosition(i);

                PlaceToken(new Vector2(position.x, position.y));
            }
        }

        private void PlaceToken(Vector2 position) {
            Instantiate(Resources.Load<GameObject>("Prefabs/Token"), position, Quaternion.identity);
        }
    }   
}
