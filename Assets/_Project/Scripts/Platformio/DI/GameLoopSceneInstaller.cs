using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private Transform levelRoot;

        public override void InstallBindings()
        {
            Container.Bind<PlayerStats>().AsSingle();
            
            // TODO Wtf with imports here?
            Container.BindFactory<Level.Level.Settings, Level.Level, Level.Level.Factory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<LevelInstaller>(levelPrefab)
                .UnderTransform(levelRoot);
        }
    }
}