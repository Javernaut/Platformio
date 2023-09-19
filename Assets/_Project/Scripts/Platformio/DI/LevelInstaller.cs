using Zenject;

namespace Platformio.DI
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [Inject]
        Level.Level.Settings _levelSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_levelSettings).WhenInjectedInto<Level.Level>();
        }
    }
}