namespace RunUp.NToken {
    public interface ICollectionObservable {
        public void SubscribeToCollection(ICollectionObserver observer);
    }
}