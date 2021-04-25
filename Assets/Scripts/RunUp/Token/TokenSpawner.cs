using System.Collections.Generic;
using Dreamteck;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace RunUp.Token {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;

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
            Instantiate(Resources.Load<GameObject>("Prefabs/Token"), position, Quaternion.identity);
        }
    }   
}
