using UnityEngine;

namespace Platformio.Player
{
    public class Bullet : MonoBehaviour
    {
        // TODO Inject a soundPlayer here and play spawn and hit sounds
        [SerializeField] private float bulletSpeed = 20f;

        private Rigidbody2D myRigidbody;
        private PlayerController player;
        private float xSpeed;

        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            player = FindObjectOfType<PlayerController>();
            xSpeed = player.transform.localScale.x * bulletSpeed;
        }

        private void Update()
        {
            myRigidbody.velocity = new Vector2(xSpeed, 0f);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}