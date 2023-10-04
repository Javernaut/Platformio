using System;
using Platformio.Loop;
using Platformio.Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Platformio.Character
{
    /// <summary>
    /// Main controller class for the Player character.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float climbSpeed = 5f;
        [SerializeField] private Vector2 deathKick = new(10f, 10f);
        [SerializeField] private Transform gun;
        private float _initialGravityScale;
        private bool _isAlive = true;

        [Inject] private LaserProjectile.Factory _laserProjectileFactory;

        private Vector2 _moveInput;
        private Animator _myAnimator;
        private CapsuleCollider2D _myBodyCollider;
        private BoxCollider2D _myFeetCollider;

        private Rigidbody2D _myRigidbody;
        [Inject] private PlayerAppearance _playerAppearance;

        [Inject] private PlayerStats _playerStats;

        [Inject] private SoundPlayer _soundPlayer;

        // Only one consumer is enough
        public Action OnStepMade;

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody2D>();
            _myRigidbody = GetComponent<Rigidbody2D>();
            _myAnimator = GetComponent<Animator>();
            _myBodyCollider = GetComponent<CapsuleCollider2D>();
            _myFeetCollider = GetComponent<BoxCollider2D>();

            _initialGravityScale = _myRigidbody.gravityScale;
        }

        private void Start()
        {
            _myAnimator.runtimeAnimatorController = _playerAppearance.AnimatorController;
        }

        private void Update()
        {
            if (!_isAlive) return;

            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }

        private void OnDisable()
        {
            // Stopping the player's movement
            _moveInput = Vector2.zero;
            Run();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Bouncing")) _soundPlayer.PlayJumpSound();
        }

        // Animation event
        private void OnStep()
        {
            if (_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) OnStepMade?.Invoke();
        }

        private void Run()
        {
            var playerVelocity = new Vector2(_moveInput.x * runSpeed, _myRigidbody.velocity.y);
            _myRigidbody.velocity = playerVelocity;

            var playerHasHorizontalSpeed = Mathf.Abs(_myRigidbody.velocity.x) > Mathf.Epsilon;
            _myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        }

        private void ClimbLadder()
        {
            if (!_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                _myRigidbody.gravityScale = _initialGravityScale;
                _myAnimator.SetBool("isClimbing", false);
                return;
            }


            var climbVelocity = new Vector2(_myRigidbody.velocity.x, _moveInput.y * climbSpeed);
            _myRigidbody.velocity = climbVelocity;
            _myRigidbody.gravityScale = 0f;

            var playerHasVerticalSpeed = Mathf.Abs(_myRigidbody.velocity.y) > Mathf.Epsilon;
            _myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        }

        private void OnMove(InputValue inputValue)
        {
            if (!_isAlive) return;

            _moveInput = inputValue.Get<Vector2>();
        }

        private void OnFire(InputValue value)
        {
            if (!_isAlive) return;

            _laserProjectileFactory.Create(gun.position, transform.localScale.x);
        }


        private void OnJump(InputValue value)
        {
            if (!_isAlive) return;

            if (!_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;


            if (value.isPressed)
            {
                _myRigidbody.velocity += new Vector2(0f, jumpSpeed);
                _soundPlayer.PlayJumpSound();
            }
        }


        private void FlipSprite()
        {
            var playerHasHorizontalSpeed = Mathf.Abs(_myRigidbody.velocity.x) > Mathf.Epsilon;

            if (playerHasHorizontalSpeed) transform.localScale = new Vector2(Mathf.Sign(_myRigidbody.velocity.x), 1f);
        }

        private void Die()
        {
            if (_myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")) && _isAlive)
            {
                _isAlive = false;
                _myAnimator.SetTrigger("Dying");
                _myRigidbody.velocity = deathKick;
                _playerStats.TakeLife();
            }
        }

        public void Reload(Vector2 newPosition, Vector3 newLocalScale)
        {
            gameObject.SetActive(false);

            _moveInput = Vector2.zero;
            transform.localScale = newLocalScale;
            transform.position = newPosition;
            _myRigidbody.velocity = Vector2.zero;

            _myAnimator.Rebind();

            _isAlive = true;

            gameObject.SetActive(true);
        }
    }
}