using Platformio.Loop;
using Platformio.Music;
using UnityEngine;
using Zenject;

namespace Platformio.Pickup
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] int pointsForCoinPickup = 100;

        [Inject] private PlayerStats _playerStats;
        [Inject] private SoundManager _soundManager;

        private bool _wasCollected;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_wasCollected)
            {
                _wasCollected = true;
                _playerStats.AddScore(pointsForCoinPickup);
                _soundManager.PlayCoinAcquiredSound();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}