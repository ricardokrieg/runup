using System.Collections.Generic;
using UnityEngine;

namespace RunUp {
    public class Container {
        private static Container instance;

        private readonly Dictionary<string, object> _bindings;
        private readonly List<NInitializer.IInitializable> _initializableBindings;
        
        public static Container Instance {
            get { return instance ??= new Container(); }
        }
        
        private Container() {
            Debug.Log("[Container] Constructor");
            
            _bindings = new Dictionary<string, object>();
            _initializableBindings = new List<NInitializer.IInitializable>();
        }

        public void Bind<T>(T binding) {
            Debug.Log("[Container] Bind " + typeof(T).FullName);
            
            _bindings[typeof(T).FullName] = binding;
        }

        public T Get<T>() {
            Debug.Log("[Container] Get " + typeof(T).FullName);
            
            return (T) _bindings[typeof(T).FullName];
        }

        public void BindInitializable(NInitializer.IInitializable initializable) {
            Debug.Log("[Container] BindInitializable " + initializable.GetType().FullName);
            
            if (_initializableBindings.Contains(initializable)) return;
            
            _initializableBindings.Add(initializable);
        }
        
        public NInitializer.IInitializable[] GetInitializables() {
            Debug.Log("[Container] GetInitializables");

            return _initializableBindings.ToArray();
        }
    }
}