namespace RunUp.NPoint {
    public interface IPointObservable {
        public void SubscribeToPoint(IPointObserver observer);
    }
}