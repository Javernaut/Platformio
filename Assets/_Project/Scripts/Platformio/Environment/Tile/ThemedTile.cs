using System;
using Platformio.DI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Platformio.Environment.Tile
{
    [CreateAssetMenu]
    public class ThemedTile : RuleTile
    {
        [SerializeField] private Type type;

        private IProvider<EnvironmentThemeConfiguration> _themeConfigurationProvider;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            _themeConfigurationProvider = tilemap.GetComponent<IProvider<EnvironmentThemeConfiguration>>();
            return true;
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            GetTileDelegate().GetTileData(position, tilemap, ref tileData);
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap,
            ref TileAnimationData tileAnimationData)
        {
            return GetTileDelegate().GetTileAnimationData(position, tilemap, ref tileAnimationData);
        }

        private TileBase GetTileDelegate()
        {
            var configuration = _themeConfigurationProvider.GetCurrentValue();
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