using UnityEngine;
using UnityEngine.UI;

namespace Platformio
{
    public class PlayerSelectionWindowController : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private GameObject _selectablePlayerPrefab;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                var item = Instantiate(_selectablePlayerPrefab, _toggleGroup.transform);
                var toggle = item.GetComponentInChildren<Toggle>();
                toggle.group = _toggleGroup;
            }
        }
    }
}