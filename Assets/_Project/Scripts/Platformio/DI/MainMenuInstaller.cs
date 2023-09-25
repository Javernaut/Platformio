using Platformio.Home.PlayerSelection;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        [SerializeField] private PlayerSelectionWindowController _playerSelectionWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<PlayerSelectionWindowController, PlayerSelectionWindowController.Factory>()
                .FromComponentInNewPrefab(_playerSelectionWindowPrefab);
        }
    }
}