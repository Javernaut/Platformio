using Platformio.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Platformio.Home.PlayerSelection
{
    /// <summary>
    /// Script for a Toggle that represents a single Player Appearance tile on the Player Selection Window.
    /// </summary>
    public class SelectablePlayer : MonoBehaviour
    {
        [SerializeField] private Image playerImage;
        [SerializeField] private Toggle toggle;

        private PlayerAppearance _playerAppearance;

        public PlayerAppearance PlayerAppearance
        {
            get => _playerAppearance;
            set
            {
                _playerAppearance = value;
                playerImage.sprite = _playerAppearance.Avatar;
            }
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            toggle.group = toggleGroup;
        }
    }
}