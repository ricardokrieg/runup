namespace RunUp.NScene {
    public interface ISceneLoadObservable {
        public void SubscribeToSceneLoaded(ISceneLoadObserver observer);
    }
}