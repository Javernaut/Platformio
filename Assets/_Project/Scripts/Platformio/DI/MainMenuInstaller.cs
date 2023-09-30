using Platformio.Home.PlayerSelection;
using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private PlayerSelectionWindowController playerSelectionWindowPrefab;

        [Inject] private GlobalMusicSettings _musicSettings;

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerSelectionWindowController, PlayerSelectionWindowController.Factory>()
                .FromComponentInNewPrefab(playerSelectionWindowPrefab);
            
            Container.BindInstance(_musicSettings.mainMenuMusic.GetRandomItem())
                .WhenInjectedInto<MusicPlayer>();
        }
    }
}