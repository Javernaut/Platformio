using Platformio.Loop;
using Platformio.Sound;
using UnityEngine;
using Zenject;

namespace Platformio.Score
{
    /// <summary>
    /// Encapsulates a score number that will be added to <see cref="PlayerStats"/> once
    /// the Player enters the adjacent collider-trigger.
    /// </summary>
    public class ScorePickup : MonoBehaviour
    {
        [Min(1)] [SerializeField] private int score = 100;

        [Inject] private PlayerStats _playerStats;
        [Inject] private SoundPlayer _soundPlayer;

        private bool _wasCollected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_wasCollected)
            {
                _wasCollected = true;

                _playerStats.AddScore(score);
                _soundPlayer.PlayScorePickupAcquiredSound();

                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}