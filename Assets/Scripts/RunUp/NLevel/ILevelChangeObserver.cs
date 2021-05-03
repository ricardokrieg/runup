namespace RunUp.NLevel {
    public interface ILevelChangeObserver {
        public void OnLevelChange(int previousLevel, int nextLevel);
    }
}