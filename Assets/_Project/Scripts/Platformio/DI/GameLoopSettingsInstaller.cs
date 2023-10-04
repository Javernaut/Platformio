using Platformio.Level;
using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    /// <summary>
    /// Adjustable settings for the Game Loop context.
    /// </summary>
    [CreateAssetMenu(
        fileName = "GameLoopSettingsInstaller",
        menuName = "DI/New GameLoopSettingsInstaller",
        order = 0)]
    public class GameLoopSettingsInstaller : ScriptableObjectInstaller<GameLoopSettingsInstaller>
    {
        public PlayerStats.Settings playerStatsSettings;
        public LevelGenerator.Settings levelConfigurationSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerStatsSettings);
            Container.BindInstance(levelConfigurationSettings);

            Container.Bind<LevelGenerator>().AsSingle();
        }
    }
}