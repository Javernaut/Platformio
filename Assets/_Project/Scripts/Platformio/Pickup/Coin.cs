using UnityEngine;

namespace Platformio.Pickup
{
    public class Coin : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}