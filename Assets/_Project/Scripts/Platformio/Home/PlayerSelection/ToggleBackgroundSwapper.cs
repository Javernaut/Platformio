using UnityEngine;
using UnityEngine.UI;

namespace Platformio.Home.PlayerSelection
{
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