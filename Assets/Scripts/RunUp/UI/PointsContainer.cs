using TMPro;
using UnityEngine;

namespace RunUp.UI {
    public class PointsContainer : MonoBehaviour, NPoint.IPointObserver {
        public void Start() {
            Debug.Log("[PointsContainer] Start");
            
            var pointObservable = Container.Instance.Get<NPoint.IPointObservable>();
            pointObservable.SubscribeToPoint(this);
            
            OnPoint(0, 100);
        }

        public void OnPoint(int pointsBefore, int points) {
            Text().text = points.ToString();
        }

        private TextMeshProUGUI Text() {
            return transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }
    }
}