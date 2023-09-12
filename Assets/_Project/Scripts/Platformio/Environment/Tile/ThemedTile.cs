using System;
using Platformio.DI;
using UnityEngine;
using UnityEngine.Serialization;
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
            GetTileDelegate().GetTileData(position, tilemap, ref tileData);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap,
            ref TileAnimationData tileAnimationData)
        {
            EnsureConfigurationProviderAvailability(tilemap);
            // TODO Consider just returning false and skipping the rule matching step
            return GetTileDelegate().GetTileAnimationData(position, tilemap, ref tileAnimationData);
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
    }
}