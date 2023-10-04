using UnityEngine;

namespace Platformio.Score
{
    /// <summary>
    /// Keeps track of the highest scored number. Persists the number in <see cref="PlayerPrefs"/>.
    /// </summary>
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