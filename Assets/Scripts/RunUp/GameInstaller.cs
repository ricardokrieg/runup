using RunUp.Audio;
using UnityEngine;
using Zenject;

namespace RunUp {
    public class GameInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container
                .Bind<AudioSource>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container
                .Bind<Canvas>()
                .WithId("Main Menu")
                .FromComponentInNewPrefabResource("Prefabs/Menu")
                .AsSingle()
                .NonLazy();
            
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

            Container
                .Bind<Player.Player>()
                .FromComponentInNewPrefabResource("Prefabs/Player")
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<GameObject>()
                .WithId("Token")
                .FromResource("Prefabs/Token");
        }
    }
}
