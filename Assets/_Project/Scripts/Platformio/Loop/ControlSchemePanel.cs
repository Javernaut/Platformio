using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Loop
{
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
            // schemeText.text = GetDeviceNameBy(newDeviceType);
            var currentControlScheme = controlSchemes[(int)newDeviceType];
            jumpControlImage.sprite = currentControlScheme.jumpButton;
            fireControlImage.sprite = currentControlScheme.fireButton;
            menuControlImage.sprite = currentControlScheme.menuButton;
        }

        private string GetDeviceNameBy(PlayerInputDeviceType type)
        {
            return type switch
            {
                PlayerInputDeviceType.Keyboard => "Keyboard",
                PlayerInputDeviceType.Playstation => "Playstation",
                PlayerInputDeviceType.Xbox => "Xbox",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown PlayerInputDeviceType")
            };
        }
    }
}