using System.Collections.Generic;
using UnityEngine;

namespace RunUp {
    public class Container {
        private static Container instance;

        private Dictionary<string, object> _bindings;
        
        public static Container Instance {
            get { return instance ??= new Container(); }
        }
        
        private Container() {
            Debug.Log("[Container] Constructor");
            
            _bindings = new Dictionary<string, object>();
        }

        public void Bind<T>(T binding) {
            Debug.Log("[Container] Bind " + typeof(T).FullName);
            
            _bindings[typeof(T).FullName] = binding;
        }

        public T Get<T>() {
            Debug.Log("[Container] Get " + typeof(T).FullName);
            
            return (T) _bindings[typeof(T).FullName];
        }
    }
}