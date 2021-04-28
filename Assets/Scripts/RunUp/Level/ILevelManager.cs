namespace RunUp.Level {
    public interface ILevelManager {
        public void LoadCurrentLevel();
        public void NextLevel();
        public void SubscribeToLevelChange(ILevelChangeObserver observer);
    }
}