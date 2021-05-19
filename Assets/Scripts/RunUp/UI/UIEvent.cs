namespace RunUp.UI {
    public struct UIEvent {
        public enum Type {
            StartGame,
            RestartGame,
            NextLevel,
            Claim,
        }

        public Type type;
    }
}