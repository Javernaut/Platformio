using Platformio.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Platformio.Home.PlayerSelection
{
    public class SelectablePlayer : MonoBehaviour
    {
        [SerializeField] private Image playerImage;
        [SerializeField] private Toggle toggle;
        
        public void SetPlayerAppearance(PlayerAppearance playerAppearance)
        {
            playerImage.sprite = playerAppearance.Avatar;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            toggle.group = toggleGroup;
        }
    }
}