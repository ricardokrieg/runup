using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace RunUp.NToken {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;
        [SerializeField] private GameObject tokenPrefab;

        public void Start() {
            var splineComputer = FindObjectOfType<SplineComputer>();

            var positions = new List<Vector3>();
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                positions.Add(splineComputer.EvaluatePosition(i));
            }

            foreach (var position in positions.ToArray()) {
                PlaceToken(position);
            }
        }

        private void PlaceToken(Vector3 position) {
            var tokenGameObject = Instantiate(tokenPrefab, position, Quaternion.identity);
            tokenGameObject.transform.parent = gameObject.transform;
        }
    }
}
