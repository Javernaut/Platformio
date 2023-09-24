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
        
        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
            Container.BindInstance(availablePlayerAppearances);
            Container.Bind<PlayerAppearanceChoiceKeeper>().AsSingle();
            
            Container.Bind<PlayerAppearance>().FromMethod(ctx =>
                ctx.Container.Resolve<PlayerAppearanceChoiceKeeper>().GetChoice()
            ).AsTransient();
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