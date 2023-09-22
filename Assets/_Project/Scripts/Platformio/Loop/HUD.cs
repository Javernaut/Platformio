using TMPro;
using UnityEngine;
using Zenject;

namespace Platformio.Loop
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI livesText;
        [SerializeField] TextMeshProUGUI scoreText;

        [Inject] private PlayerStats _playerStats;

        private void OnEnable()
        {
            _playerStats.OnLivesNumberChanged += ProcessPlayerDeath;
            _playerStats.OnScoreChanged += OnScoreChanged;

            livesText.text = _playerStats.PlayerLives.ToString();
            scoreText.text = _playerStats.Score.ToString();
        }

        private void OnDisable()
        {
            _playerStats.OnLivesNumberChanged -= ProcessPlayerDeath;
            _playerStats.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int newScore)
        {
            scoreText.text = newScore.ToString();
        }

        private void ProcessPlayerDeath(int newLives)
        {
            livesText.text = newLives.ToString();
        }
    }
}