using Platformio.Environment.Tile;
using Platformio.Sound;
using Zenject;

namespace Platformio.DI
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        [Inject] private Level.Level.Settings _levelSettings;
        [Inject] private SoundPlayer _soundPlayer;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelSettings.theme).WhenInjectedInto<TilemapThemeProvider>();
            
            _soundPlayer.OverrideStepsSounds(_levelSettings.theme.stepsSounds);
        }
    }
}