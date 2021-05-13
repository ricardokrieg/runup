namespace RunUp.NPoint {
    public interface IPointStore {
        public int LoadPoints();
        public void SavePoints(int points);
    }
}