using System;
using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Platformio.Environment.Tile
{
    [ExecuteAlways]
    public class TilemapThemeProvider : MonoBehaviour, IProvider<EnvironmentThemeConfiguration>
    {
        private EnvironmentThemeConfiguration _environmentThemeConfiguration;

        private Tilemap _tilemap;

        [Inject]
        public void Construct(EnvironmentThemeConfiguration environmentThemeConfiguration)
        {
            Debug.Log("E name " + environmentThemeConfiguration.name);
            _environmentThemeConfiguration = environmentThemeConfiguration;
        }

        private void Awake()
        {
            Debug.Log("E name Awake");
            EnsureTileMapAvailability();
            _tilemap.RefreshAllTiles();
        }

        private void Start()
        {
            Debug.Log("E name Start");
        }

        private void EnsureTileMapAvailability()
        {
            _tilemap ??= GetComponent<Tilemap>();
        }

        public EnvironmentThemeConfiguration GetCurrentValue()
        {
            Debug.Log("GetCurrentValue");
            return _environmentThemeConfiguration;
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                EnsureTileMapAvailability();
                // TODO WTF should be here?
                // _tilemap.RefreshAllTiles();
            }
        }
    }
}