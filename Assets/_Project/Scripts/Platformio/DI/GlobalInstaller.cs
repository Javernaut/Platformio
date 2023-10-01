using Platformio.Home.PlayerSelection;
using Platformio.Player;
using Platformio.Score;
using Platformio.Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Platformio.DI
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        [SerializeField] private InputActionAsset globalInputActionAsset;
        [SerializeField] private PlayerAppearance[] availablePlayerAppearances;
        [SerializeField] private GlobalMusicSettings musicSettings;
        [SerializeField] private SoundPlayer.Settings soundSettings;

        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
            Container.Bind<PlayerAppearanceChoiceKeeper>().AsSingle();

            Container.BindInstance(availablePlayerAppearances);
            Container.BindInstance(musicSettings);
            Container.BindInstance(soundSettings);
            Container.BindInstance(globalInputActionAsset);
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