using Platformio.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Home.PlayerSelection
{
    public class PlayerSelectionWindowController : MonoBehaviour
    {
        [Inject] private PlayerAppearance[] _playerAppearances;
        [Inject] private PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;

        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private SelectablePlayer selectablePlayerPrefab;

        public delegate void SelectionConfirmed();

        public event SelectionConfirmed OnSelectionConfirmed;
        
        private void Start()
        {
            foreach (var playerAppearance in _playerAppearances)
            {
                var selectablePlayer = Instantiate(selectablePlayerPrefab, toggleGroup.transform);
                selectablePlayer.SetToggleGroup(toggleGroup);
                selectablePlayer.PlayerAppearance = playerAppearance;
            }
        }

        public void OnBackPressed()
        {
            Destroy(gameObject);
        }

        public void OnPlayerSelectedPressed()
        {
            var selectedPlayer = toggleGroup.GetFirstActiveToggle().GetComponent<SelectablePlayer>();
            _playerAppearanceChoiceKeeper.SetChoice(selectedPlayer.PlayerAppearance);

            OnSelectionConfirmed?.Invoke();
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<PlayerSelectionWindowController>
        {
        }
    }
}