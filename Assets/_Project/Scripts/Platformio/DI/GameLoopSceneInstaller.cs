using Platformio.Home.PlayerSelection;
using Platformio.Loop;
using Platformio.Player;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [Inject] private GameLoopSettingsInstaller.LevelConfigurationSettings _settings;
        [Inject] private PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;
        
        [SerializeField] private PlayerAppearance _fallbackPlayerAppearance;
        [SerializeField] private Transform levelRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInstance(_playerAppearanceChoiceKeeper.GetChoice() ?? _fallbackPlayerAppearance);
            
            Container.BindFactory<Level.Level.Settings, Level.Level, Level.Level.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<LevelInstaller>(_ =>
                    _settings.levelPrefabs.GetRandomItem()
                )
                .UnderTransform(levelRoot);
        }
    }
}