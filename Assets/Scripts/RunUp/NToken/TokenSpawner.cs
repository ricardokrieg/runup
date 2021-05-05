using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace RunUp.NToken {
    public class TokenSpawner : MonoBehaviour {
        [SerializeField] private int quantity = 12;
        [SerializeField] private GameObject tokenPrefab;
        // TODO program to interface
        [SerializeField] private Token finalToken;

        private ICollectionObserver _observer;

        public void Start() {
            // TODO program to interface
            _observer = Container.Instance.Get<GameManager>();

            var splineComputer = FindObjectOfType<SplineComputer>();

            var positions = new List<Vector3>();
            var step = 1f / quantity;
            for (var i = step; i < 1; i += step) {
                positions.Add(splineComputer.EvaluatePosition(i));
            }

            foreach (var position in positions.ToArray()) {
                PlaceToken(position);
            }
            
            SubscribeObserver(finalToken);
        }

        private void PlaceToken(Vector3 position) {
            var tokenGameObject = Instantiate(tokenPrefab, position, Quaternion.identity);
            tokenGameObject.transform.parent = gameObject.transform;
            
            // TODO program to interface (Token is an implementation)
            var observable = tokenGameObject.GetComponent<Token>() as ICollectionObservable;
            SubscribeObserver(observable);
        }

        private void SubscribeObserver(ICollectionObservable observable) {
            observable.SubscribeToCollection(_observer);
        }
    }
}
