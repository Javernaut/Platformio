using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.Character
{
    /// <summary>
    /// Main controller for a laser projectile that Player shoots.
    /// </summary>
    public class LaserProjectile : MonoBehaviour
    {
        [SerializeField] private float startSpeed = 20f;
        [Inject] private readonly float _playerXScale;

        [Inject] private readonly SoundPlayer _soundPlayer;
        [Inject] private readonly Vector3 _startPosition;

        private Rigidbody2D _myRigidbody;
        private float _xSpeed;

        private void Awake()
        {
            _myRigidbody = GetComponent<Rigidbody2D>();
            transform.position = _startPosition;
            _soundPlayer.PlayLaserShotSound();
        }

        private void Start()
        {
            _xSpeed = _playerXScale * startSpeed;
        }

        private void Update()
        {
            _myRigidbody.velocity = new Vector2(_xSpeed, 0f);
        }

        private void OnDestroy()
        {
            _soundPlayer.PlayLaserHitSound();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                _soundPlayer.PlayEnemyHitSound();
            }

            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Vector3, float, LaserProjectile>
        {
        }
    }
}