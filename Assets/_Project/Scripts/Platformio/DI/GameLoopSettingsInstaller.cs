using System;
using Platformio.Environment;
using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    // TODO Add better menu name and file name
    [CreateAssetMenu]
    public class GameLoopSettingsInstaller : ScriptableObjectInstaller<GameLoopSettingsInstaller>
    {
        public PlayerStats.Settings playerStatsSettings;
        public ThemeConfiguration themeConfiguration;

        public override void InstallBindings()
        {
            Container.BindInstance(playerStatsSettings);
            Container.BindInstance(themeConfiguration);
        }

        [Serializable]
        public class ThemeConfiguration
        {
            public EnvironmentThemeConfiguration[] themes;
        }
    }
}