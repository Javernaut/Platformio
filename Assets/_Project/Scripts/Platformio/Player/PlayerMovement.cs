using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformio.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 moveInput;
        
        private void OnMove(InputValue inputValue)
        {
            moveInput = inputValue.Get<Vector2>();
        }
    }
}