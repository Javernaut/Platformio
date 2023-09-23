using Platformio.Pickup;
using Zenject;

namespace Platformio.DI
{
    public class GlobalInstaller : MonoInstaller<GlobalInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().AsSingle();
        }
    }
}