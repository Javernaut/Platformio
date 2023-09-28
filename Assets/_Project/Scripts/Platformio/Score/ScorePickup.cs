using Platformio.Loop;
using Platformio.Music;
using UnityEngine;
using Zenject;

namespace Platformio.Score
{
    public class ScorePickup : MonoBehaviour
    {
        [Min(1)] [SerializeField] private int score = 100;

        [Inject] private PlayerStats _playerStats;
        [Inject] private SoundManager _soundManager;

        private bool _wasCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_wasCollected)
            {
                _wasCollected = true;

                _playerStats.AddScore(score);
                _soundManager.PlayScorePickupAcquiredSound();

                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}