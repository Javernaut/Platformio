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
        [Inject] private readonly PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;
        [Inject] private readonly GlobalMusicSettings _musicSettings;
        [Inject] private readonly LevelGenerator _levelGenerator;

        [SerializeField] private PlayerAppearance fallbackPlayerAppearance;
        [SerializeField] private Transform levelRoot;
        [SerializeField] private GameObject levelAnnouncementPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInstance(_playerAppearanceChoiceKeeper?.GetChoice() ?? fallbackPlayerAppearance);

            Container.BindInstance(_musicSettings.gameLoopMusic.GetRandomItem())
                .WhenInjectedInto<MusicPlayer>();

            Container.BindFactory<LevelFacade, LevelFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewPrefabMethod(_levelGenerator.GetLevelPrefab, _levelGenerator.InjectLevelGameObject)
                .UnderTransform(levelRoot)
                .AsSingle();

            Container.Bind<LevelAnnouncement>().AsTransient();
            Container.BindFactory<int, LevelAnnouncement, LevelAnnouncement.Factory>()
                .FromComponentInNewPrefab(levelAnnouncementPrefab);

            Container.BindInterfacesAndSelfTo<PlayerInputDeviceTracker>().AsSingle();
        }
    }
}