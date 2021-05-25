using RunUp.NClaim;
using UnityEngine;
using UnityEngine.UI;

namespace RunUp.UI {
    public class ClaimButton : MonoBehaviour {
        private ClaimManager _claimManager;
        
        public void Start() {
            // TODO program to interface
            _claimManager = Container.Instance.Get<ClaimManager>();
            
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick() {
            Debug.Log("[ClaimButton] OnClick");
            
            if (!_claimManager.IsAvailable()) return;

            _claimManager.ClaimPoints();
        }
    }
}