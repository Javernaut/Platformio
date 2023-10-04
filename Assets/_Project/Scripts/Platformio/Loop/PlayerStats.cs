using System;
using Platformio.Score;
using UnityEngine;
using Zenject;

namespace Platformio.Loop
{
    public class PlayerStats : IDisposable
    {
        public delegate void LivesNumberChanged(int newLives);

        public delegate void ScoreChanged(int newLives);

        [Inject] private ScoreCounter _scoreCounter;

        [Inject]
        public PlayerStats(Settings settings)
        {
            PlayerLives = settings.initialLives;
            Score = settings.initialScore;
        }

        public int PlayerLives { get; private set; }

        public int Score { get; private set; }

        public void Dispose()
        {
            _scoreCounter.OfferNewMaxScore(Score);
        }

        public event LivesNumberChanged OnLivesNumberChanged;

        public event ScoreChanged OnScoreChanged;

        public void TakeLife()
        {
            if (PlayerLives > 0)
            {
                PlayerLives--;
                OnLivesNumberChanged?.Invoke(PlayerLives);
            }
        }

        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke(Score);
        }

        [Serializable]
        public class Settings
        {
            [Min(1)] public int initialLives;
            [Min(0)] public int initialScore;
        }
    }
}