using Platformio.Home.PlayerSelection;
using Platformio.Level;
using Platformio.Loop;
using Platformio.Player;
using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [Inject] private PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;
        [Inject] private GlobalMusicSettings _musicSettings;

        [SerializeField] private PlayerAppearance fallbackPlayerAppearance;
        [SerializeField] private Transform levelRoot;

        [Inject] private readonly LevelGenerator _levelGenerator;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInstance(_playerAppearanceChoiceKeeper.GetChoice() ?? fallbackPlayerAppearance);

            Container.BindInstance(_musicSettings.gameLoopMusic.GetRandomItem())
                .WhenInjectedInto<MusicPlayer>();
            
            Container.BindFactory<LevelFacade, LevelFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabMethod(_levelGenerator.GetLevelPrefab, _levelGenerator.InjectLevelGameObject)
                .UnderTransform(levelRoot);
        }
    }
}