using Platformio.Home.PlayerSelection;
using Platformio.Pickup;
using Platformio.Player;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        [SerializeField] private PlayerAppearance[] availablePlayerAppearances;
        // TODO Consider moving it into a scene context of the main menu
        [SerializeField] private PlayerSelectionWindowController _playerSelectionWindowPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
            Container.BindInstance(availablePlayerAppearances);
            Container.Bind<PlayerAppearanceChoiceKeeper>().AsSingle();
            
            Container.Bind<PlayerAppearance>().FromMethod(ctx =>
                ctx.Container.Resolve<PlayerAppearanceChoiceKeeper>().GetChoice()
            ).AsTransient();
            
            // TODO Consider moving it into a scene context of the main menu
            Container.BindFactory<PlayerSelectionWindowController, PlayerSelectionWindowController.Factory>()
                .FromComponentInNewPrefab(_playerSelectionWindowPrefab);
        }
    }

    public static class Utils
    {
        public static T GetRandomItem<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}