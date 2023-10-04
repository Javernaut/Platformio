using Platformio.Sound;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment
{
    [CreateAssetMenu(fileName = "EnvironmentThemeConfiguration", menuName = "Environment Theme Configuration",
        order = 0)]
    public class EnvironmentThemeConfiguration : ScriptableObject
    {
        public TileBase thinPlatformTile;
        public TileBase thickPlatformTile;
        public TileBase roundedPlatformTile;
        public TileBase cutPlatformTile;
        public TileBase blockPlatformTile;
        public TileBase uncutBlockTile;

        public SoundBank stepsSounds;

        public Sprite background;
    }
}