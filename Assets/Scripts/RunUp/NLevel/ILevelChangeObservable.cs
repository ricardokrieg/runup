namespace RunUp.NLevel {
    public interface ILevelChangeObservable {
        public void SubscribeToLevelChange(ILevelChangeObserver observer);
    }
}