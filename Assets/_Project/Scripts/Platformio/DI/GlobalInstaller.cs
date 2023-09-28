using Platformio.Home.PlayerSelection;
using Platformio.Music;
using Platformio.Player;
using Platformio.Score;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        [SerializeField] private PlayerAppearance[] availablePlayerAppearances;
        [SerializeField] private MusicManager.Settings musicSettings;
        [SerializeField] private SoundManager.Settings soundSettings;

        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
            Container.Bind<PlayerAppearanceChoiceKeeper>().AsSingle();

            Container.BindInstance(availablePlayerAppearances);
            Container.BindInstance(musicSettings);
            Container.BindInstance(soundSettings);
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