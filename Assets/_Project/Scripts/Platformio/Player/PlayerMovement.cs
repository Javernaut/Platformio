using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformio.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] float jumpSpeed = 5f;
        [SerializeField] float climbSpeed = 5f;

        private Vector2 moveInput;
        private Rigidbody2D _myRigidbody;
        private Animator _myAnimator;
        private CapsuleCollider2D _myCapsuleCollider;

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody2D>();
            _myRigidbody = GetComponent<Rigidbody2D>();
            _myAnimator = GetComponent<Animator>();
            _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        }

        private void Update()
        {
            Run();
            FlipSprite();
            ClimbLadder();
        }

        private void Run()
        {
            var playerVelocity = new Vector2(moveInput.x * runSpeed, _myRigidbody.velocity.y);
            _myRigidbody.velocity = playerVelocity;

            var playerHasHorizontalSpeed = Mathf.Abs((_myRigidbody).velocity.x) > Mathf.Epsilon;
            _myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        }

        private void ClimbLadder()
        {
            if (!_myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                return;
            }

            Vector2 climbVelocity = new Vector2(_myRigidbody.velocity.x, moveInput.y * climbSpeed);
            _myRigidbody.velocity = climbVelocity;
        }


        private void OnMove(InputValue inputValue)
        {
            moveInput = inputValue.Get<Vector2>();
        }

        void OnJump(InputValue value)
        {
            if (!_myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return;
            }

            if (value.isPressed)
            {
                // TODO Shouldn't it be applyForce(impulse)?
                _myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }


        private void FlipSprite()
        {
            var playerHasHorizontalSpeed = Mathf.Abs((_myRigidbody).velocity.x) > Mathf.Epsilon;

            if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(_myRigidbody.velocity.x), 1f);
            }
        }
    }
}