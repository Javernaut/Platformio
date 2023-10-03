using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.Player
{
    public class LaserProjectile : MonoBehaviour
    {
        [SerializeField] private float startSpeed = 20f;

        [Inject] private readonly SoundPlayer _soundPlayer;
        [Inject] private readonly float _playerXScale;
        [Inject] private readonly Vector3 _startPosition;

        private Rigidbody2D _myRigidbody;
        private float _xSpeed;

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody2D>();
            transform.position = _startPosition;
        }

        private void Start()
        {
            _xSpeed = _playerXScale * startSpeed;
        }

        private void Update()
        {
            // TODO Let's make it constant, perhaps?
            // Shall we have a kinematic rb in this case?
            _myRigidbody.velocity = new Vector2(_xSpeed, 0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                _soundPlayer.PlayEnemyHitSound();
            }

            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            // TODO Play laser hit sound
        }

        public class Factory : PlaceholderFactory<Vector3, float, LaserProjectile>
        {
        }
    }
}