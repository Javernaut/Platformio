using UnityEngine;

namespace Platformio.Player
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        private Rigidbody2D _myRigidbody;
    
        void Start()
        {
            _myRigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
        }

        void OnTriggerExit2D(Collider2D other) 
        {
            if (!other.CompareTag("Player"))
            {
                moveSpeed = -moveSpeed;
                FlipEnemyFacing();    
            }
        }

        void FlipEnemyFacing()
        {
            transform.localScale = new Vector2 ((Mathf.Sign(_myRigidbody.velocity.x)), 1f);
        }

    }
}