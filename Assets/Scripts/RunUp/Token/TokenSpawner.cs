using System.Collections.Generic;
using Dreamteck;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;

        private Token.Factory _tokenFactory;
        
        [Inject]
        public void Init(Token.Factory tokenFactory) {
            Debug.Log("[TokenSpawner] Init");
            _tokenFactory = tokenFactory;
        }
        
        public void Start() {
            var splineComputer = FindObjectOfType<SplineComputer>();

            var positions = new List<Vector3>();
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                positions.Add(splineComputer.EvaluatePosition(i));
            }

            positions.RemoveAt(0);
            positions.RemoveAt(positions.Count - 1);
            foreach (var position in positions.ToArray()) {
                PlaceToken(position);
            }
        }

        private void PlaceToken(Vector3 position) {
            var token = _tokenFactory.Create("Prefabs/Token");
            token.transform.position = position;
        }
    }   
}
