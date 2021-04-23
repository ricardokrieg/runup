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
                .Bind<AudioSource>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            // Container
            //     .Bind<Canvas>()
            //     .WithId("Main Menu")
            //     .FromComponentInNewPrefabResource("Prefabs/Menu")
            //     .AsSingle()
            //     .NonLazy();
            //
            // Container
            //     .Bind<GameObject>()
            //     .WithId("Claim Screen")
            //     .FromResource("Prefabs/ClaimScreen");
            
            Container
                .Bind<AudioService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Audio.AudioSettings>()
                .AsSingle();

            Container
                .Bind<AudioClip>()
                .WithId("Main Theme")
                .FromResource("Sounds/Casual Title PIANO LOOP na Casual Game Music")
                .AsSingle();

            // Container
            //     .Bind<Player.Player>()
            //     .FromComponentInNewPrefabResource("Prefabs/Player")
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}
