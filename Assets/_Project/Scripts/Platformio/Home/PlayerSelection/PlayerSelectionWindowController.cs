using Platformio.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Home.PlayerSelection
{
    public class PlayerSelectionWindowController : MonoBehaviour
    {
        [Inject] private PlayerAppearance[] _playerAppearances;

        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private SelectablePlayer selectablePlayerPrefab;

        // Start is called before the first frame update
        void Start()
        {
            for (var pos = 0; pos < _playerAppearances.Length; pos++)
            {
                var selectablePlayer = Instantiate(selectablePlayerPrefab, toggleGroup.transform);
                selectablePlayer.SetToggleGroup(toggleGroup);
                selectablePlayer.SetPlayerAppearance(_playerAppearances[pos]);
            }
        }
    }
}