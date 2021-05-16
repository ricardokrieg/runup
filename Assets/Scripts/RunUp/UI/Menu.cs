using UnityEngine;

namespace RunUp.UI {
    public class Menu : MonoBehaviour {
        public void Start() {
            DontDestroyOnLoad(gameObject);
        }

        public void HideMenu() {
            MenuGameObject().SetActive(false);
        }

        public void ShowMenu() {
            MenuGameObject().SetActive(true);
        }

        public void HidePointsPanel() {
            PointsPanelGameObject().SetActive(false);
        }

        public void ShowPointsPanel() {
            PointsPanelGameObject().SetActive(true);
        }

        private GameObject MenuGameObject() {
            return transform.Find("Menu").gameObject;
        }
        
        private GameObject PointsPanelGameObject() {
            return transform.Find("PointsPanel").gameObject;
        }
    }
}