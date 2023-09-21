using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Platformio.Environment.Tile
{
    [ExecuteAlways]
    public class TilemapThemeProvider : MonoBehaviour, IProvider<EnvironmentThemeConfiguration>
    {
        [SerializeField] private EnvironmentThemeConfiguration fallbackEnvironmentThemeConfiguration;

        private EnvironmentThemeConfiguration _environmentThemeConfiguration;

        private Tilemap _tilemap;

        [Inject]
        public void Construct(EnvironmentThemeConfiguration environmentThemeConfiguration)
        {
            _environmentThemeConfiguration = environmentThemeConfiguration;
        }

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public EnvironmentThemeConfiguration GetCurrentValue()
        {
            return Application.IsPlaying(gameObject)
                ? _environmentThemeConfiguration
                : fallbackEnvironmentThemeConfiguration;
        }

        private void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                _tilemap.RefreshAllTiles();
            }
        }
    }
}