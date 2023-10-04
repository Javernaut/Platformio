using TMPro;
using UnityEngine;

namespace Platformio.UI
{
    /// <summary>
    /// TextMeshPro label that can change its color based on the enclosing <see cref="SelectionStateExposingButton"/>
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextColorSwitcher : MonoBehaviour
    {
        [SerializeField] private SelectionStateExposingButton button;

        [SerializeField] private Color normalColor;
        [SerializeField] private Color highlightedColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color disabledColor;

        private TextMeshProUGUI _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
            button ??= GetComponentInParent<SelectionStateExposingButton>();
            // TODO Fail here if button is null?
        }

        private void Start()
        {
            FallbackIfClear(ref highlightedColor);
            FallbackIfClear(ref pressedColor);
            FallbackIfClear(ref selectedColor);
            FallbackIfClear(ref disabledColor);
        }

        private void OnEnable()
        {
            button.OnNewPublicSelectionState += OnNewButtonState;
            ApplyButtonState(button.CurrentPublicSelectionState);
        }

        private void OnDisable()
        {
            button.OnNewPublicSelectionState -= OnNewButtonState;
        }

        private void OnNewButtonState(PublicSelectionState newState)
        {
            ApplyButtonState(newState);
        }

        private void ApplyButtonState(PublicSelectionState state)
        {
            _textComponent.color = state switch
            {
                PublicSelectionState.Highlighted => highlightedColor,
                PublicSelectionState.Pressed => pressedColor,
                PublicSelectionState.Selected => selectedColor,
                PublicSelectionState.Disabled => disabledColor,
                // The rest is treated as Normal
                _ => normalColor
            };
        }

        private void FallbackIfClear(ref Color color)
        {
            if (color == Color.clear) color = normalColor;
        }
    }
}