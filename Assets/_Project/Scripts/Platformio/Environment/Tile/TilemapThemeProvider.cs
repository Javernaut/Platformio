using Platformio.DI;
using UnityEngine;

namespace Platformio.Environment.Tile
{
    public class TilemapThemeProvider : MonoBehaviour, IProvider<EnvironmentThemeConfiguration>
    {
        [SerializeField] private EnvironmentThemeConfiguration environmentThemeConfiguration;

        public EnvironmentThemeConfiguration GetCurrentValue()
        {
            return environmentThemeConfiguration;
        }
    }
}