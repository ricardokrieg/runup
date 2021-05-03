namespace RunUp.UI {
    public struct UIEvent {
        public enum Type {
            StartGame,
            RestartGame,
            NextLevel,
        }

        public Type type;
    }
}