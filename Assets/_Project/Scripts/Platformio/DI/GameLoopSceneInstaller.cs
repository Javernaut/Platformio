using Platformio.Character;
using Platformio.Home.PlayerSelection;
using Platformio.Level;
using Platformio.Loop;
using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    /// <summary>
    /// DI injection rules for the Game Loop scene context.
    /// </summary>
    public class GameLoopSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerAppearance fallbackPlayerAppearance;
        [SerializeField] private Transform levelRoot;
        [SerializeField] private GameObject levelAnnouncementPrefab;
        [SerializeField] private GameObject laserProjectilePrefab;
        [Inject] private readonly LevelGenerator _levelGenerator;
        [Inject] private readonly GlobalMusicSettings _musicSettings;
        [Inject] private readonly PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInstance(_playerAppearanceChoiceKeeper?.GetChoice() ?? fallbackPlayerAppearance);

            Container.BindInstance(_musicSettings.gameLoopMusic)
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

            Container.BindFactory<Vector3, float, LaserProjectile, LaserProjectile.Factory>()
                .FromComponentInNewPrefab(laserProjectilePrefab)
                .AsSingle();
        }
    }
}