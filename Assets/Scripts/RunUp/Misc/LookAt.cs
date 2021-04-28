using System;
using UnityEngine;

namespace RunUp.Misc {
    public class LookAt : MonoBehaviour {
        [SerializeField] private Transform target;

        private Camera _camera;

        public void Start() {
            _camera = GetComponent<Camera>();
        }

        public void LateUpdate() {
            _camera.transform.LookAt(target);
        }
    }
}