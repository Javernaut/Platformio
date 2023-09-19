using UnityEngine;
using Zenject;

namespace Platformio.DI
{
    [CreateAssetMenu]
    public class GameLoopSettingsInstaller : ScriptableObjectInstaller<GameLoopSettingsInstaller>
    {
        public GameSession.Settings gameSessionSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSessionSettings);
        }
    }
}