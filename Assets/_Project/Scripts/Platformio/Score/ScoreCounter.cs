using UnityEngine;

namespace Platformio.Score
{
    public class ScoreCounter
    {
        private const string KeyLastMaxScore = "key_last_max_score";

        public int MaxScore => PlayerPrefs.GetInt(KeyLastMaxScore, 0);

        public void OfferNewMaxScore(int newMaxScore)
        {
            if (newMaxScore > MaxScore) PlayerPrefs.SetInt(KeyLastMaxScore, newMaxScore);
        }
    }
}