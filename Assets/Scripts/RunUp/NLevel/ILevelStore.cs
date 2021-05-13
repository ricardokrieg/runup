namespace RunUp.NLevel {
    public interface ILevelStore {
        public int LoadLevel();
        public void SaveLevel(int level);
    }
}