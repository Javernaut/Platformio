using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentThemeConfiguration", menuName = "Create Environment Theme Configuration",
        order = 0)]
    public class EnvironmentThemeConfiguration : ScriptableObject
    {
        public TileBase thinPlatformTile;
    }
}