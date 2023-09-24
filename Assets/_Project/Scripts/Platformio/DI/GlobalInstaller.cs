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
            // TODO properly pick the valid implementation according to the User's choice
            Container.Bind<PlayerAppearance>().FromMethod(() =>
                availablePlayerAppearances.GetRandomItem()
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