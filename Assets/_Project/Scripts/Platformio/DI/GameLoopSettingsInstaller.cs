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
        public LevelConfigurationSettings levelConfigurationSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerStatsSettings);
            Container.BindInstance(levelConfigurationSettings);
        }

        [Serializable]
        public class LevelConfigurationSettings
        {
            public EnvironmentThemeConfiguration[] themes;
            public GameObject[] levelPrefabs;
        }
    }
}