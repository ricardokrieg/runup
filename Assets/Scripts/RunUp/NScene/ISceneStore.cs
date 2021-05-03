using System.Collections.Generic;

namespace RunUp.NScene {
    public interface ISceneStore {
        public void AddScene(string sceneName);
        public void RemoveAllExcept(string keepSceneName);
        public void ListLoadedScenes();
        public bool SceneIsLoaded(string sceneName);
        public IEnumerable<string> GetSceneNames();
    }
}