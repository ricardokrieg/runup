using TMPro;
using UnityEngine;

namespace RunUp.NClaim {
    public class ClaimText : MonoBehaviour {
        public void Start() {
            Debug.Log("[ClaimText] Start");

            var claimManager = Container.Instance.Get<ClaimManager>();

            GetComponent<TextMeshPro>().text = "+" + claimManager.AvailablePoints();
        }
    }
}