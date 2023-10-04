using Platformio.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Platformio.Home.PlayerSelection
{
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