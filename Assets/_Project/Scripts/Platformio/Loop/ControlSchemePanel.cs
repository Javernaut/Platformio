using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Loop
{
    /// <summary>
    /// The UI controller for a Control Scheme overlay during the Game Loop.
    /// </summary>
    public class ControlSchemePanel : MonoBehaviour
    {
        // Assume the order just as in PlayerInputDeviceType
        [SerializeField] private ControlScheme[] controlSchemes;
        [SerializeField] private Image jumpControlImage;
        [SerializeField] private Image fireControlImage;
        [SerializeField] private Image menuControlImage;
        [Inject] private readonly PlayerInputDeviceTracker _playerInputDeviceTracker;

        private void Awake()
        {
            SetNewDeviceType(_playerInputDeviceTracker.CurrentDevice);
            _playerInputDeviceTracker.OnPlayerInputDeviceType += SetNewDeviceType;
        }

        private void OnDestroy()
        {
            _playerInputDeviceTracker.OnPlayerInputDeviceType -= SetNewDeviceType;
        }

        private void SetNewDeviceType(PlayerInputDeviceType newDeviceType)
        {
            var currentControlScheme = controlSchemes[(int)newDeviceType];
            jumpControlImage.sprite = currentControlScheme.jumpButton;
            fireControlImage.sprite = currentControlScheme.fireButton;
            menuControlImage.sprite = currentControlScheme.menuButton;
        }
    }
}