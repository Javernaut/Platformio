using Platformio.DI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Platformio.Environment.Tile
{
    /// <summary>
    /// A component that is used to provide <see cref="ThemeConfiguration"/> to the <see cref="ThemedTile"/>
    /// in the adjacent Tilemap. This component is used instead of Extenject, because the <see cref="ThemedTile"/>
    /// are (1) being heavily reused and (2) are serialized in prefab where the <see cref="ThemeConfiguration"/> is
    /// intended to be overridden during the runtime. 
    /// </summary>
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