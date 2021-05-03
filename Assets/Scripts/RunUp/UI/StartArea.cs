using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RunUp.UI {
    public class StartArea : Button, IEventObservable {
        private List<IEventObserver> _observers;

        // TODO what method should I use as this is a Button? Maybe onEnable?
        public void Start() {
            _observers = new List<IEventObserver>();

            // TODO find how to invert this dependency? Should I call Container here?
            var gameManager = Container.Instance.Get<GameManager>();
            Subscribe(gameManager);
        }
        
        public void Subscribe(IEventObserver observer) {
            if (_observers.Contains(observer)) return;
            
            _observers.Add(observer);
        }
        
        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            gameObject.GetComponentInParent<Canvas>().enabled = false;
            
            NotifyObservers();
        }
        
        private void NotifyObservers() {
            foreach (var observer in _observers.ToArray()) {
                if (!_observers.Contains(observer)) continue;
                
                var uiEvent = new UIEvent() {
                    type = UIEvent.Type.StartGame
                };
                observer.OnEvent(uiEvent);
            }
        }
    }
}
