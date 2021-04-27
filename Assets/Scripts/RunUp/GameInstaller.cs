using Dreamteck.Splines;
using RunUp.Audio;
using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
                .BindInterfacesAndSelfTo<GameManager>()
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
                .Bind<Level.ILevelProvider>()
                .To<Level.LevelManager>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<AudioService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<Audio.AudioSettings>()
                .AsSingle();
        }
    }
}
