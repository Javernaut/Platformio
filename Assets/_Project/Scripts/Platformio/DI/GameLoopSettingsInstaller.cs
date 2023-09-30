using Platformio.Level;
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
        public LevelGenerator.Settings levelConfigurationSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerStatsSettings);
            Container.BindInstance(levelConfigurationSettings);
            
            Container.Bind<LevelGenerator>().AsSingle();
        }
    }
}