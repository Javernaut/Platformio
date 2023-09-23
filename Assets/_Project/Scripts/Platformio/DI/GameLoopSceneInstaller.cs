using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [Inject] private GameLoopSettingsInstaller.LevelConfigurationSettings _settings;
        
        [SerializeField] private Transform levelRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();

            // TODO Wtf with imports here?
            Container.BindFactory<Level.Level.Settings, Level.Level, Level.Level.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<LevelInstaller>(_ =>
                    _settings.levelPrefabs[Random.Range(0, _settings.levelPrefabs.Length)]
                )
                .UnderTransform(levelRoot);
        }
    }
}