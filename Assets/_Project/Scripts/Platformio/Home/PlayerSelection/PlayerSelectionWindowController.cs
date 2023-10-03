using Platformio.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Home.PlayerSelection
{
    public class PlayerSelectionWindowController : MonoBehaviour
    {
        [Inject] private readonly PlayerAppearance[] _playerAppearances;
        [Inject] private readonly PlayerAppearanceChoiceKeeper _playerAppearanceChoiceKeeper;
        [Inject] private readonly InputActionAsset _globalInputActionAsset;

        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private SelectablePlayer selectablePlayerPrefab;
        [SerializeField] private GameObject confirmButton;

        private PreviousSelectionPreserver _selectionPreserver;

        public delegate void SelectionConfirmed();

        public event SelectionConfirmed OnSelectionConfirmed;

        private void Start()
        {
            _selectionPreserver = PreviousSelectionPreserver.ReplaceWith(confirmButton);
            foreach (var playerAppearance in _playerAppearances)
            {
                var selectablePlayer = Instantiate(selectablePlayerPrefab, toggleGroup.transform);
                selectablePlayer.SetToggleGroup(toggleGroup);
                selectablePlayer.PlayerAppearance = playerAppearance;
            }

            _globalInputActionAsset["UI/Cancel"].performed += onCancelActionPerformed;
        }

        private void OnDestroy()
        {
            _globalInputActionAsset["UI/Cancel"].performed -= onCancelActionPerformed;
        }

        private void onCancelActionPerformed(InputAction.CallbackContext _)
        {
            OnBackPressed();
        }

        public void OnBackPressed()
        {
            Destroy(gameObject);
            _selectionPreserver.RestorePreviousSelection();
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

    class PreviousSelectionPreserver
    {
        private readonly GameObject _lastSelectedGameObject;

        private PreviousSelectionPreserver(GameObject lastSelectedGameObject)
        {
            _lastSelectedGameObject = lastSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(_lastSelectedGameObject);
        }

        public void RestorePreviousSelection()
        {
            EventSystem.current.SetSelectedGameObject(_lastSelectedGameObject);
        }

        public static PreviousSelectionPreserver ReplaceWith(GameObject newObjectToSelect)
        {
            var result = new PreviousSelectionPreserver(EventSystem.current.currentSelectedGameObject);
            EventSystem.current.SetSelectedGameObject(newObjectToSelect);
            return result;
        }
    }
}