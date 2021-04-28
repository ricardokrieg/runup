namespace RunUp.Token {
    public interface IToken {
        public void SubscribeToCollect(ICollectObserver observer);
    }
}