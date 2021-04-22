using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class StartArea : MonoBehaviour {
        void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            gameObject.GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
