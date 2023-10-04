using Platformio.Sound;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment
{
    /// <summary>
    /// Aggregates a theme-specific values like
    /// the exact <see cref="TileBase"/> objects, sounds of steps and the background image used on Main Menu.
    /// </summary>
    [CreateAssetMenu(
        fileName = "NewThemeConfiguration",
        menuName = "Theme/Theme Configuration",
        order = 0)]
    public class ThemeConfiguration : ScriptableObject
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