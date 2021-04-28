namespace RunUp.Level {
    public interface ILevelChangeObserver {
        public void OnLevelChange(int previousLevel, int nextLevel);
    }
}