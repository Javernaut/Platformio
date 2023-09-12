using System;
using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformio.Environment.Tile
{
    [ExecuteAlways]
    public class TilemapThemeProvider : MonoBehaviour, IProvider<EnvironmentThemeConfiguration>
    {
        [SerializeField] private EnvironmentThemeConfiguration environmentThemeConfiguration;

        private Tilemap _tilemap;

        private void Awake()
        {
            EnsureTileMapAvailability();
        }

        private void EnsureTileMapAvailability()
        {
            _tilemap ??= GetComponent<Tilemap>();
        }

        public EnvironmentThemeConfiguration GetCurrentValue()
        {
            return environmentThemeConfiguration;
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                EnsureTileMapAvailability();
                _tilemap.RefreshAllTiles();
            }
        }
    }
}