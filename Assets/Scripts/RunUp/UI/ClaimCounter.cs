using System.Security.Claims;
using RunUp.NClaim;
using TMPro;
using UnityEngine;

namespace RunUp.UI {
    public class ClaimCounter : MonoBehaviour {
        private ClaimManager _claimManager;
        private long _time = 600;
        private bool shouldHideClaimValue = true;
        
        public void Start() {
            // TODO program to interface
            _claimManager = Container.Instance.Get<ClaimManager>();
        }

        public void Update() {
            var secondsToAvailable = _claimManager.SecondsToAvailable();

            if (_time == secondsToAvailable) return;
            _time = secondsToAvailable;
            
            if (_time == 0) {
                GetComponent<TextMeshProUGUI>().text = "Claim";
                var claimValue = ClaimValue();
                claimValue.text = "+" + _claimManager.AvailablePoints();
                claimValue.enabled = true;
                shouldHideClaimValue = true;
            } else  {
                if (shouldHideClaimValue) {
                    shouldHideClaimValue = false;
                    ClaimValue().enabled = false;
                }
                
                var minutesText = _time / 60;
                var secondsText = _time % 60;

                GetComponent<TextMeshProUGUI>().text =
                    minutesText.ToString().PadLeft(2, '0') +
                    ":" +
                    secondsText.ToString().PadLeft(2, '0');
            }
        }

        private TextMeshProUGUI ClaimValue() {
            return transform.parent.Find("ClaimCounter2").GetComponent<TextMeshProUGUI>();
        }
    }
}