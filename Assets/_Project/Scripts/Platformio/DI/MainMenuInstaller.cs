using Platformio.Home.PlayerSelection;
using Platformio.Music;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private PlayerSelectionWindowController _playerSelectionWindowPrefab;

        [Inject] private MusicManager.Settings musicSettings;

        public override void InstallBindings()
        {
            Container.BindFactory<PlayerSelectionWindowController, PlayerSelectionWindowController.Factory>()
                .FromComponentInNewPrefab(_playerSelectionWindowPrefab);
            
            Container.BindInstance(musicSettings.mainMenuMusic.GetRandomItem())
                .WhenInjectedInto<MusicManager>();

            Container.BindInterfacesTo<MusicManager>().AsSingle().NonLazy();
        }
    }
}