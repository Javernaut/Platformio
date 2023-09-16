using UnityEngine;

namespace Platformio.Pickup
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip coinPickupSFX;
        
        void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
                Destroy(gameObject);
            }
        }
    }
}