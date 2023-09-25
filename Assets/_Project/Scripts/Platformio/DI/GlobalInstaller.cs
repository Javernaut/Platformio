using Platformio.Home.PlayerSelection;
using Platformio.Music;
using Platformio.Pickup;
using Platformio.Player;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        [SerializeField] private PlayerAppearance[] availablePlayerAppearances;
        [SerializeField] private MusicManager.Settings musicSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
            Container.Bind<PlayerAppearanceChoiceKeeper>().AsSingle();
            Container.BindInstance(availablePlayerAppearances);
            Container.BindInstance(musicSettings);
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