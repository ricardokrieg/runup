using UnityEngine;
using UnityEngine.UI;

namespace RunUp.UI {
    public class SpriteSwapToggle : MonoBehaviour {
        [SerializeField] private string backgroundName = "Background";

        private void Start() {
            GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool isOn) {
            BackgroundImage().enabled = !isOn;
        }

        private Transform Background() {
            return transform.Find(backgroundName);
        }

        private Image BackgroundImage() {
            return Background().gameObject.GetComponent<Image>();        
        }
    }
}
