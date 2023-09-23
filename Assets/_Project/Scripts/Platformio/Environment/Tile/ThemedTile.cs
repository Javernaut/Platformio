using JetBrains.Annotations;
using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment.Tile
{
    // TODO Add better menu name and file name
    [CreateAssetMenu]
    public class ThemedTile : TileBase
    {
        [SerializeField] private Type type;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            GetTileDelegate(tilemap)?.GetTileData(position, new ProxyTilemap(tilemap), ref tileData);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap,
            ref TileAnimationData tileAnimationData)
        {
            return GetTileDelegate(tilemap)?
                .GetTileAnimationData(position, new ProxyTilemap(tilemap), ref tileAnimationData) ?? false;
        }

        [CanBeNull]
        private TileBase GetTileDelegate(ITilemap tilemap)
        {
            // TODO Consider caching of the providers, perhaps along with ProxyTilemap
            // Can't cache the provider as is, because the ThemedTile is shared across multiple tilemaps with potentially different themes
            var themeConfigurationProvider = tilemap.GetComponent<IProvider<EnvironmentThemeConfiguration>>();
            if (themeConfigurationProvider == null)
            {
                // TODO The themeConfigurationProvider may be null in case the method is called via
                // Platformio.Environment.Tile.ThemedTile.GetTileData (UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.Tilemaps.TileData& tileData) (at Assets/_Project/Scripts/Platformio/Environment/Tile/ThemedTile.cs:14)
                // Platformio.Environment.Tile.ThemedTile.GetTileDelegate (UnityEngine.Tilemaps.ITilemap tilemap) (at Assets/_Project/Scripts/Platformio/Environment/Tile/ThemedTile.cs:28)
                // UnityEditor.AssetPreviewUpdater:CreatePreviewForAsset(Object, Object[], String) (at /Users/bokken/build/output/unity/unity/Editor/Mono/AssetPreviewUpdater.cs:14)
                // The preview of a tile is crippled (returned as null, so no preview for this type of tile will be drawn)
                return null;
            }

            var configuration = themeConfigurationProvider.GetCurrentValue();
            if (configuration == null)
            {
                // TODO In Editor tiles want to be rendered even before TilemapThemeProvider.Awake.
                return null;
            }
            return type switch
            {
                Type.ThinPlatform => configuration.thinPlatformTile,
                Type.ThickPlatform => configuration.thickPlatformTile,
                Type.RoundedPlatform => configuration.roundedPlatformTile,
                Type.CutPlatform => configuration.cutPlatformTile,
                Type.BlockPlatform => configuration.blockPlatformTile,
                // TODO Consider another fallback
                _ => configuration.thinPlatformTile
            };
        }

        // TODO Consider using something like this instead of an enum
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
        enum Type
        {
            ThinPlatform,
            ThickPlatform,
            RoundedPlatform,
            CutPlatform,
            BlockPlatform,
        }

        // TODO Make a cache of sort. Same Theme tile may be used in multiple Tilemaps, so the cache has to be done on a per-tilemap basis
        private class ProxyTilemap : ITilemap
        {
            private readonly ITilemap _tilemap;

            public ProxyTilemap(ITilemap tilemap) : base(tilemap.GetComponent<Tilemap>())
            {
                _tilemap = tilemap;
            }

            public override TileBase GetTile(Vector3Int position)
            {
                var tileBase = base.GetTile(position);

                return (tileBase as ThemedTile)?.GetTileDelegate(_tilemap) ?? tileBase;
            }

            public override T GetTile<T>(Vector3Int position)
            {
                return GetTile(position) as T;
            }
        }
    }
}