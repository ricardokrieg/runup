using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunUp.Scene {
    public class SceneStore {
        private List<string> _sceneNames;
        
        public SceneStore() {
            _sceneNames = new List<string>();
        }
        
        public void AddScene(string sceneName) {
            if (_sceneNames.Contains(sceneName)) return;
            
            _sceneNames.Add(sceneName);
        }
        
        public void ListSceneNames() {
            Debug.Log("[SceneStore] List of scenes:");
            
            for (var i = 0; i < SceneManager.sceneCount; ++i) {
                var scene = SceneManager.GetSceneAt(i);
                var output = scene.name;
                output += scene.isLoaded ? " (Loaded, " : " (Not Loaded, ";
                output += scene.isDirty ? "Dirty, " : "Clean, ";
                output += scene.buildIndex >= 0 ? "in build)" : "NOT in build)";
                
                Debug.Log("[SceneStore] " + output);
            }
        }

        public bool SceneIsLoaded(string sceneName) {
            for (var i = 0; i < SceneManager.sceneCount; ++i) {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName) return true;
            }

            return false;
        }

        public IEnumerable<string> GetSceneNames() {
            return _sceneNames.ToArray();
        }
    }
}