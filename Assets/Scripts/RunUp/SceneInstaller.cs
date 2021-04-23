using Dreamteck.Splines;
using Zenject;

namespace RunUp {
    public class SceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
                .Bind<SplineComputer>()
                .FromComponentInHierarchy()
                .AsCached();
        }
    }
}
