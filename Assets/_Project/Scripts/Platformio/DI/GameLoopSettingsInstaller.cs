using Platformio.Loop;
using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    [CreateAssetMenu]
    public class GameLoopSettingsInstaller : ScriptableObjectInstaller<GameLoopSettingsInstaller>
    {
        public PlayerStats.Settings playerStatsSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerStatsSettings);
        }
    }
}