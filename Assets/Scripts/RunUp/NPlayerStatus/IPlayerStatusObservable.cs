namespace RunUp.NPlayerStatus {
    public interface IPlayerStatusObservable {
        public void SubscribeToWin(IPlayerStatusObserver observer);
        public void SubscribeToLoss(IPlayerStatusObserver observer);
    }
}