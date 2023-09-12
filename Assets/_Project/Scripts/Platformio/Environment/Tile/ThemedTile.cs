using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment.Tile
{
    [CreateAssetMenu]
    public class ThemedTile : TileBase
    {
        [SerializeField] private Type type;

        private IProvider<EnvironmentThemeConfiguration> _themeConfigurationProvider;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            EnsureConfigurationProviderAvailability(tilemap);
            GetTileDelegate().GetTileData(position, new ProxyTilemap(tilemap), ref tileData);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap,
            ref TileAnimationData tileAnimationData)
        {
            EnsureConfigurationProviderAvailability(tilemap);
            return GetTileDelegate().GetTileAnimationData(position, new ProxyTilemap(tilemap), ref tileAnimationData);
        }

        private void EnsureConfigurationProviderAvailability(ITilemap tilemap)
        {
            _themeConfigurationProvider ??= tilemap.GetComponent<IProvider<EnvironmentThemeConfiguration>>();
        }

        private TileBase GetTileDelegate()
        {
            var configuration = _themeConfigurationProvider?.GetCurrentValue();
            return type switch
            {
                Type.ThinPlatform => configuration.thinPlatformTile,
                // TODO Consider another fallback
                _ => configuration.thinPlatformTile
            };
        }

        // TODO Consider using something like this instead of an enum
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
        enum Type
        {
            ThinPlatform
        }

        // TODO Make a cache of sort. Same Theme tile may be used in multiple Tilemaps, so the cache has to be done on a per-tilemap basis
        private class ProxyTilemap : ITilemap
        {
            public ProxyTilemap(ITilemap tilemap) : base(tilemap.GetComponent<Tilemap>())
            {
            }

            public override TileBase GetTile(Vector3Int position)
            {
                var tileBase = base.GetTile(position);

                return (tileBase as ThemedTile)?.GetTileDelegate() ?? tileBase;
            }

            public override T GetTile<T>(Vector3Int position)
            {
                return GetTile(position) as T;
            }
        }
    }
}