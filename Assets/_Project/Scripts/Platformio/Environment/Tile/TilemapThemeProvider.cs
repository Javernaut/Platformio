using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Platformio.Environment.Tile
{
    [ExecuteAlways]
    [RequireComponent(typeof(Tilemap))]
    public class TilemapThemeProvider : MonoBehaviour, IProvider<EnvironmentThemeConfiguration>
    {
        [InjectOptional]
        [SerializeField] private EnvironmentThemeConfiguration environmentThemeConfiguration;
        
        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        private void Start()
        {
            _tilemap.RefreshAllTiles();
        }

        public EnvironmentThemeConfiguration GetCurrentValue() => environmentThemeConfiguration;

        private void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                _tilemap.RefreshAllTiles();
            }
        }
    }
}