using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Zenject;

namespace Platformio.Loop
{
    public class PlayerInputDeviceTracker : IInitializable, IDisposable
    {
        public delegate void PlayerInputDeviceTypeChanged(PlayerInputDeviceType newDeviceType);

        [Inject] private readonly PlayerInput _playerInput;

        public PlayerInputDeviceType CurrentDevice { get; private set; }

        public void Dispose()
        {
            InputUser.onChange -= OnInputUserChange;
        }

        public void Initialize()
        {
            CurrentDevice = GetDeviceByUser(_playerInput.user);
            InputUser.onChange += OnInputUserChange;
        }

        public event PlayerInputDeviceTypeChanged OnPlayerInputDeviceType;

        private void OnInputUserChange(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                CurrentDevice = GetDeviceByUser(user);
                OnPlayerInputDeviceType?.Invoke(CurrentDevice);
            }
        }

        private PlayerInputDeviceType GetDeviceByUser(InputUser inputUser)
        {
            var devices = inputUser.pairedDevices;

            if (devices.Any(
                    item =>
                        item.name.Contains("DualSense") ||
                        item.name.Contains("DualShock")
                ))
                return PlayerInputDeviceType.Playstation;

            if (devices.Any(
                    item => item.name.Contains("Xbox"))
               )
                return PlayerInputDeviceType.Xbox;

            // The fallback value is the keyboard
            return PlayerInputDeviceType.Keyboard;
        }
    }
}