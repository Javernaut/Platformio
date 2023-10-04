using Platformio.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Platformio.Loop
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image playerAvatar;
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [Inject] private PlayerAppearance _playerAppearance;

        [Inject] private PlayerStats _playerStats;

        private void Start()
        {
            playerAvatar.sprite = _playerAppearance.Avatar;
        }

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