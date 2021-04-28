using RunUp.Audio;
using Zenject;

namespace RunUp {
    public class GlobalInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
                .BindInterfacesAndSelfTo<GameManager>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Level.ILevelManager>()
                .To<Level.LevelManager>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Player.PlayerManager>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Scene.SceneLoader>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<AudioService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<Audio.AudioSettings>()
                .AsSingle();
            
            Container
                .BindFactory<string, Token.Token, Token.Token.Factory>()
                .FromFactory<PrefabResourceFactory<Token.Token>>();
            
            Container
                .BindFactory<string, Player.Player, Player.Player.Factory>()
                .FromFactory<PrefabResourceFactory<Player.Player>>();
        }
    }
}
