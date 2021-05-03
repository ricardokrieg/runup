namespace RunUp.NObstacle {
    public interface ICollisionObservable {
        public void SubscribeToCollision(ICollisionObserver observer);
    }
}