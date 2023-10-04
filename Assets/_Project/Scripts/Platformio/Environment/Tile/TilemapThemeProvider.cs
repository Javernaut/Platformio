using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Platformio.Environment.Tile
{
    [ExecuteAlways]
    [RequireComponent(typeof(Tilemap))]
    public class TilemapThemeProvider : MonoBehaviour, IProvider<ThemeConfiguration>
    {
        [InjectOptional] [SerializeField] private ThemeConfiguration themeConfiguration;

        private Tilemap _tilemap;

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        private void Start()
        {
            _tilemap.RefreshAllTiles();
        }

        private void Update()
        {
            if (!Application.IsPlaying(gameObject)) _tilemap.RefreshAllTiles();
        }

        public ThemeConfiguration GetCurrentValue()
        {
            return themeConfiguration;
        }
    }
}