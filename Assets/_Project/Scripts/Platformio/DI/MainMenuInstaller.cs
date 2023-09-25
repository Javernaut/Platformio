using Platformio.Home.PlayerSelection;
using Platformio.Music;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private PlayerSelectionWindowController playerSelectionWindowPrefab;

        [Inject] private MusicManager.Settings _musicSettings;

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerSelectionWindowController, PlayerSelectionWindowController.Factory>()
                .FromComponentInNewPrefab(playerSelectionWindowPrefab);
            
            Container.BindInstance(_musicSettings.mainMenuMusic.GetRandomItem())
                .WhenInjectedInto<MusicManager>();
        }
    }
}