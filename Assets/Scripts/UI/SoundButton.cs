using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SoundButton : MonoBehaviour
    {
        private void Start() {
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Debug.Log("Sound click");
        }
    }
}
