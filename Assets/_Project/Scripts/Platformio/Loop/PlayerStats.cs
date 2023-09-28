using System;
using Platformio.Score;
using UnityEngine;
using Zenject;

namespace Platformio.Loop
{
    public class PlayerStats : IDisposable
    {
        private int _playerLives;
        private int _score;

        public int PlayerLives => _playerLives;
        public int Score => _score;

        [Inject] private ScoreCounter _scoreCounter;

        public delegate void LivesNumberChanged(int newLives);

        public event LivesNumberChanged OnLivesNumberChanged;

        public delegate void ScoreChanged(int newLives);

        public event ScoreChanged OnScoreChanged;

        [Inject]
        public PlayerStats(Settings settings)
        {
            _playerLives = settings.initialLives;
            _score = settings.initialScore;
        }

        [Serializable]
        public class Settings
        {
            [Min(1)] public int initialLives;
            [Min(0)] public int initialScore;
        }

        public void TakeLife()
        {
            if (_playerLives > 0)
            {
                _playerLives--;
                OnLivesNumberChanged?.Invoke(_playerLives);
            }
        }

        public void AddScore(int score)
        {
            _score += score;
            OnScoreChanged?.Invoke(_score);
        }

        public void Dispose()
        {
            _scoreCounter.OfferNewMaxScore(_score);
        }
    }
}