using UnityEngine;

namespace Platformio.Pickup
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip coinPickupSFX;
        [SerializeField] int pointsForCoinPickup = 100;

        private bool _wasCollected;
        
        void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Player") && !_wasCollected)
            {
                _wasCollected = true;
                FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
                AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}