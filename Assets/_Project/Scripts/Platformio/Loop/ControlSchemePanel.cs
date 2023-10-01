using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Platformio.Loop
{
    public class ControlSchemePanel : MonoBehaviour
    {
        [Inject] private readonly PlayerInputDeviceTracker _playerInputDeviceTracker;

        [SerializeField] private TextMeshProUGUI schemeText;

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
            schemeText.text = GetDeviceNameBy(newDeviceType);
        }

        private string GetDeviceNameBy(PlayerInputDeviceType type)
        {
            return type switch
            {
                PlayerInputDeviceType.Keyboard => "Keyboard",
                PlayerInputDeviceType.PS => "Playstation",
                PlayerInputDeviceType.Xbox => "Xbox",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown PlayerInputDeviceType")
            };
        }
    }
}