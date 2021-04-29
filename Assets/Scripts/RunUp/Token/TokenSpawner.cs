using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;

        private GameManager _gameManager;
        private List<Token> _tokens;

        [Inject]
        public void Init(GameManager gameManager) {
            Debug.Log("[TokenSpawner] Init");

            _gameManager = gameManager;
        }
        
        public void Start() {
            _tokens = new List<Token>();
            
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

        public void OnDisable() {
            foreach (var token in _tokens.ToArray()) {
                Destroy(token.gameObject);
            }
        }

        private void PlaceToken(Vector3 position) {
            var tokenPrefab = Resources.Load<GameObject>("Prefabs/Token");
            var tokenGameObject = Instantiate(tokenPrefab, position, Quaternion.identity);
            var token = tokenGameObject.gameObject.GetComponent<Token>();
            
            _tokens.Add(token);
            token.Register(_gameManager);
        }
    }   
}
