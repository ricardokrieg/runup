using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RunUp.UI {
    public class ContinueButton : MonoBehaviour, IEventObservable {
        private List<IEventObserver> _observers;
        
        public void Start() {
            _observers = new List<IEventObserver>();
            
            // TODO program to interface
            var observer = Container.Instance.Get<GameManager>();
            Subscribe(observer);
            
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void Subscribe(IEventObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }
        
        private void OnClick() {
            Debug.Log("[ContinueButton] OnClick");

            GetComponent<Button>().interactable = false;
            NotifyObservers();
        }
        
        private void NotifyObservers() {
            foreach (var observer in _observers.ToArray()) {
                if (!_observers.Contains(observer)) continue;
                
                var uiEvent = new UIEvent() {
                    type = UIEvent.Type.NextLevel
                };
                observer.OnEvent(uiEvent);
            }
        }
    }
}