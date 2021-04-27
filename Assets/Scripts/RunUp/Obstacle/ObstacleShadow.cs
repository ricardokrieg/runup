using System;
using UnityEngine;

namespace RunUp.Obstacle {
    public class ObstacleShadow : MonoBehaviour {
        private readonly Vector2 _offset = new Vector2(0.1f, -0.1f);
        
        private Transform _transformShadow;
        private SpriteRenderer _rendererShadow;

        private Transform _transformCaster;
        private SpriteRenderer _rendererCaster;
        
        public void Start() {
            _transformCaster = transform.Find("Sprite");
            _rendererCaster = _transformCaster.gameObject.GetComponent<SpriteRenderer>();
            
            _transformShadow = new GameObject().transform;
            _transformShadow.parent = _transformCaster;
            _transformShadow.gameObject.name = "Shadow";
            _transformShadow.localRotation = Quaternion.identity;
            _transformShadow.localScale = new Vector3(1, 1, 1);

            _rendererShadow = _transformShadow.gameObject.AddComponent<SpriteRenderer>();
            _rendererShadow.sprite = _rendererCaster.sprite;
            _rendererShadow.sortingLayerName = _rendererCaster.sortingLayerName;
            _rendererShadow.sortingOrder = _rendererCaster.sortingOrder - 1;
            _rendererShadow.material = _rendererCaster.material;
            _rendererShadow.color = new Color(0, 0, 0, 0.4f);
        }

        public void LateUpdate() {
            _transformShadow.position = _transformCaster.position + (Vector3)_offset;
        }
    }
}