namespace RunUp.NPlayerStatus {
    public interface IPlayerStatusObserver {
        public void OnWin();
        public void OnLoss();
    }
}
