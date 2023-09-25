using Platformio.Environment.Tile;
using Platformio.Music;
using Zenject;

namespace Platformio.DI
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        [Inject] private Level.Level.Settings _levelSettings;
        [Inject] private SoundManager _soundManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelSettings.theme).WhenInjectedInto<TilemapThemeProvider>();
            
            _soundManager.OverrideStepsSounds(_levelSettings.theme.stepsSounds);
        }
    }
}