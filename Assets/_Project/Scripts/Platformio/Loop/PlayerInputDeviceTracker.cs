using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Zenject;

namespace Platformio.Loop
{
    public class PlayerInputDeviceTracker : IInitializable, IDisposable
    {
        [Inject] private readonly PlayerInput _playerInput;

        private PlayerInputDeviceType _currentDevice;

        public PlayerInputDeviceType CurrentDevice => _currentDevice;

        public delegate void PlayerInputDeviceTypeChanged(PlayerInputDeviceType newDeviceType);

        public event PlayerInputDeviceTypeChanged OnPlayerInputDeviceType;

        public void Initialize()
        {
            _currentDevice = GetDeviceByUser(_playerInput.user);
            InputUser.onChange += OnInputUserChange;
        }

        private void OnInputUserChange(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged) {
                _currentDevice = GetDeviceByUser(user);
                OnPlayerInputDeviceType?.Invoke(_currentDevice);
            }
        }

        public void Dispose()
        {
            InputUser.onChange -= OnInputUserChange;
        }

        private PlayerInputDeviceType GetDeviceByUser(InputUser inputUser)
        {
            var devices = inputUser.pairedDevices;

            if (devices.Any(
                    item =>
                        item.name.Contains("DualSense") ||
                        item.name.Contains("DualShock")
                ))
            {
                return PlayerInputDeviceType.Playstation;
            }

            if (devices.Any(
                    item => item.name.Contains("Xbox"))
               )
            {
                return PlayerInputDeviceType.Xbox;
            }

            // The fallback value is the keyboard
            return PlayerInputDeviceType.Keyboard;
        }
    }
}