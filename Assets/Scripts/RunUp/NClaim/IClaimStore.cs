namespace RunUp.NClaim {
    public interface IClaimStore {
        public long LoadAvailableAt();
        public void Clear();
    }
}