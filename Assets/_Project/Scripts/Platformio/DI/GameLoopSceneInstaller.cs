using Platformio.Loop;
using Platformio.Player;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [Inject] private GameLoopSettingsInstaller.LevelConfigurationSettings _settings;
        // TODO Perhaps, injecting it from the global context isn't right solution
        [Inject] private PlayerAppearance _playerAppearance;

        [SerializeField] private Transform levelRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInstance(_playerAppearance);

            // TODO Wtf with imports here?
            Container.BindFactory<Level.Level.Settings, Level.Level, Level.Level.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<LevelInstaller>(_ =>
                    _settings.levelPrefabs.GetRandomItem()
                )
                .UnderTransform(levelRoot);
        }
    }
}