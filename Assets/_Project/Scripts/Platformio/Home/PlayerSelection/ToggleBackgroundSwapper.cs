using UnityEngine;
using UnityEngine.UI;

namespace Platformio.Home.PlayerSelection
{
    /// <summary>
    /// Primitive swapper of the Toggle's background sprite depending on the 'isChecked' state. 
    /// </summary>
    public class ToggleBackgroundSwapper : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Image background;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite selectedSprite;

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(ApplyState);
            ApplyState(toggle.isOn);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(ApplyState);
        }

        private void ApplyState(bool isChecked)
        {
            background.sprite = isChecked ? selectedSprite : defaultSprite;
        }
    }
}