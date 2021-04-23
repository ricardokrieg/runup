using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RunUp.UI {
    public class ClaimScreenOpener : MonoBehaviour {
        private GameObject _claimScreenPrefab;

        // [Inject]
        // public void Init([Inject(Id = "Claim Screen")] GameObject claimScreenPrefab) {
        //     _claimScreenPrefab = claimScreenPrefab;
        // }
        
        void Start() {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Instantiate(_claimScreenPrefab);
        }
    }
}
