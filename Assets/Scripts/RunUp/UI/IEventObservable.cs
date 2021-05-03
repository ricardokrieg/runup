namespace RunUp.UI {
    public interface IEventObservable {
        public void Subscribe(IEventObserver observer);
    }
}