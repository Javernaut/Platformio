using Platformio.Environment.Tile;
using Zenject;

namespace Platformio.DI
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        [Inject] private Level.Level.Settings _levelSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelSettings.theme).WhenInjectedInto<TilemapThemeProvider>();
        }
    }
}